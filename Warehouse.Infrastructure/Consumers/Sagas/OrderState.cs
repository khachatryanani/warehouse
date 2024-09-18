using MassTransit;

namespace Warehouse.Infrastructure.Consumers.Sagas
{
    public class OrderState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int Version { get; set; }
    }
}
