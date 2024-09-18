using MassTransit;
using Warehouse.Common.Contracts.Events;
using Warehouse.Common.Contracts.Messages;

namespace Warehouse.Infrastructure.Consumers.Sagas
{
    public class OrderSubmittedFlow: MassTransitStateMachine<OrderState>
    {
        public State PendingApproval { get; set; }

        public Event<OrderCreatedEvent> Created { get; set; }
        public Event<OrderApprovedEvent> Approved { get; set; }
        public Event<OrderRejectedEvent> Rejected { get; set; }

        public OrderSubmittedFlow()
        {
            InstanceState(x => x.CurrentState);

            Event(() => Created, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => Approved, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => Rejected, x => x.CorrelateById(x => x.Message.CorrelationId));

            Initially(
                When(Created)
                .Publish(c => new ReviewOrderRequest()
                {
                    CorrelationId = c.Message.CorrelationId,
                    OrderId = c.Message.OrderId,
                })
                .TransitionTo(PendingApproval));

            During(PendingApproval,
                When(Approved)
                .Publish(c => new ApproveOrderRequest()
                {
                    CorrelationId = c.Message.CorrelationId,
                    OrderId = c.Message.OrderId,
                })
                .TransitionTo(Final),

                When(Rejected)
                .Publish(c => new RejectOrderRequest()
                {
                    CorrelationId = c.Message.CorrelationId,
                    OrderId = c.Message.OrderId,
                })
                .TransitionTo(Final)
            );
        }

    }
}
