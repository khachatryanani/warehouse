using MassTransit;
using Warehouse.Common.Contracts.Messages;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Enums;

namespace Warehouse.Infrastructure.Consumers
{
    public class RejectOrderConsumer(IOrderRepository orderRepository, IStockService stockService) : IConsumer<RejectOrderRequest>
    {
        public async Task Consume(ConsumeContext<RejectOrderRequest> context)
        {
            var order = await orderRepository.GetByIdAsync(context.Message.OrderId, context.CancellationToken);

            await stockService.TakeStockItemsByProductIdAsync(order.ProductId, order.ItemsCount, context.CancellationToken);
            await orderRepository.UpdateStatusAsync(order.Id, OrderStatus.Approved, context.CancellationToken);
        }
    }
}
