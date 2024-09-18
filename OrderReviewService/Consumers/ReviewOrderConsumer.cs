using MassTransit;
using Warehouse.Common.Contracts.Events;
using Warehouse.Common.Contracts.Messages;

namespace OrderReviewService.Consumers
{
    public class ReviewOrderConsumer : IConsumer<ReviewOrderRequest>
    {
        public Task Consume(ConsumeContext<ReviewOrderRequest> context)
        {
            if (ShouldApproveOrder())
            {
                context.Publish(new OrderApprovedEvent
                {
                    CorrelationId = context.Message.CorrelationId,
                    OrderId = context.Message.OrderId,
                });
            }
            else 
            {
                context.Publish(new OrderRejectedEvent
                {
                    CorrelationId = context.Message.CorrelationId,
                    OrderId = context.Message.OrderId,
                });
            }

            return Task.CompletedTask;
        }

        private bool ShouldApproveOrder() 
        => new Random().Next(0,1) == 0;
    }
}
