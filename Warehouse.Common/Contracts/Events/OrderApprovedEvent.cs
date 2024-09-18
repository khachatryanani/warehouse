namespace Warehouse.Common.Contracts.Events
{
    public class OrderApprovedEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
    }
}
