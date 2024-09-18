using MassTransit;
using MediatR;
using Warehouse.Common.Contracts.Events;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Orders
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IStockService stockService, IPublishEndpoint bus) : IRequestHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await CreateInitialOrderAsync(request, cancellationToken);
            await ProceedOrderAsync(request, cancellationToken);
        }

        private async Task CreateInitialOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            await ValidateProduct(request.Order.ProductId, cancellationToken);

            request.Order.Status = OrderStatus.Pending;
            request.Order.Id = await orderRepository.CreateAsync(request.Order, cancellationToken);
        }

        private async Task ProceedOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var stockState = await stockService.GetStockStateByProductIdAsync(request.Order.ProductId, cancellationToken);

            switch (stockState)
            {
                case StockState.Available: await HandleAvailableStockAsync(request, cancellationToken); break;
                case StockState.LowStock: await HandleLowStockAsync(request, cancellationToken); break;
                case StockState.OutOfStock: await HandleOutOfStockAsync(request, cancellationToken); break;
            };
        }

        private async Task HandleAvailableStockAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await stockService.TakeStockItemsByProductIdAsync(request.Order.ProductId, request.Order.ItemsCount, cancellationToken);
            await orderRepository.UpdateStatusAsync(request.Order.Id, OrderStatus.Approved, cancellationToken);
        }

        private async Task HandleLowStockAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
          await bus.Publish(new OrderCreatedEvent { CorrelationId = Guid.NewGuid(), OrderId = request.Order.Id }, cancellationToken: cancellationToken);
        }

        private async Task HandleOutOfStockAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Mode is ReserveMode.None) 
            {
                await orderRepository.UpdateStatusAsync(request.Order.Id, OrderStatus.Rejected, cancellationToken);
                throw new OutOfStockException(string.Format(ErrorMessages.ProductOutOfStock, request.Order.ProductId));
            }
        }

        private async Task ValidateProduct(int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.ExistsAsync(productId, cancellationToken))
            {
                throw new InvalidStateException(ErrorMessages.InvalidProduct);
            }
        }
    }
}
