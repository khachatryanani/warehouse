using MassTransit;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Events;
using Warehouse.Domain.Resources;

namespace Warehouse.Infrastructure.Consumers
{
    public class CreateOrderConsumer(IOrderRepository orderRepository, IStockService stockService, IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var order = await orderRepository.GetByIdAsync(context.Message.OrderId, context.CancellationToken);
            var stockState = await stockService.GetStockStateByProductIdAsync(order.ProductId, context.CancellationToken);

            var OrderCreatedResponse = stockState switch
            {
                StockState.Available => await HandleAvailableStockAsync(order, context.CancellationToken),
                StockState.LowStock => await HandleLowStockAsync(order, context.CancellationToken),
                StockState.OutOfStock => await HandleOutOfStockAsync(order, context.Message.Mode, context.CancellationToken),
                _ => throw new NotImplementedException(),
            };

            await context.RespondAsync(OrderCreatedResponse);
        }

        private async Task<OrderCreatedEventResponse> HandleAvailableStockAsync(Order order, CancellationToken cancellationToken)
        {
            await stockService.TakeStockItemsByProductIdAsync(order.ProductId, order.ItemsCount, cancellationToken);
            await orderRepository.UpdateStatusAsync(order.Id, OrderStatus.Approved, cancellationToken);

            return new OrderCreatedEventResponse
            {
                OrderId = order.Id,
                Status = OrderStatus.Approved,
            };
        }

        private async Task<OrderCreatedEventResponse> HandleLowStockAsync(Order order, CancellationToken cancellationToken)
        {
            await stockService.TakeStockItemsByProductIdAsync(order.ProductId, order.ItemsCount, cancellationToken);
            await orderRepository.UpdateStatusAsync(order.Id, OrderStatus.UnderReview, cancellationToken);

            await publishEndpoint.Publish(new OrderSubmittedForReviewEvent { CorrelationId = Guid.NewGuid(), OrderId = order.Id }, cancellationToken: cancellationToken);

            return new OrderCreatedEventResponse
            {
                OrderId = order.Id,
                Status = OrderStatus.UnderReview,
            };
        }

        private async Task<OrderCreatedEventResponse> HandleOutOfStockAsync(Order order, ReserveMode mode, CancellationToken cancellationToken)
        {
            return mode switch
            {
                ReserveMode.None => await RejectOrderAsync(order, cancellationToken),
                ReserveMode.ReserveWhenAvailable => await WaitListOrderAsync(order, cancellationToken),
                _ => throw new NotImplementedException(),
            };

            async Task<OrderCreatedEventResponse> RejectOrderAsync(Order order, CancellationToken cancellationToken)
            {
                await orderRepository.UpdateStatusAsync(order.Id, OrderStatus.Rejected, cancellationToken);
                return new OrderCreatedEventResponse
                {
                    OrderId = order.Id,
                    Status = OrderStatus.Rejected,
                    ErrorMessage = string.Format(ErrorMessages.ProductOutOfStock, order.ProductId)
                };
            }

            async Task<OrderCreatedEventResponse> WaitListOrderAsync(Order order, CancellationToken cancellationToken)
            {
                await orderRepository.UpdateStatusAsync(order.Id, OrderStatus.Pending, cancellationToken);
                return new OrderCreatedEventResponse
                {
                    OrderId = order.Id,
                    Status = OrderStatus.Pending
                };
            }
        }
    }
}
