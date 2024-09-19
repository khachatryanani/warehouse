using MassTransit;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Events;

namespace Warehouse.Infrastructure.Consumers
{
    public class StockChangedConsumer(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IConsumer<StockChangedEvent>
    {
        public async Task Consume(ConsumeContext<StockChangedEvent> context)
        {
            var order = (await orderRepository.GetByProductIdAsync(context.Message.ProductId))
                                              .FirstOrDefault(o => o.Status is OrderStatus.Pending && context.Message.StockItemsCount >= o.ItemsCount);
            if (order is null)
            {
                return;
            }

            await publishEndpoint.Publish(new OrderCreatedEvent()
            {
                CorrelationId = Guid.NewGuid(),
                OrderId = order.Id,
                Mode = ReserveMode.ReserveWhenAvailable,
            }, cancellationToken: context.CancellationToken);
        }
    }
}
