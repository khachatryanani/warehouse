namespace Warehouse.Common.Contracts.Events
{
    public class OrderCreatedEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
    }
}
