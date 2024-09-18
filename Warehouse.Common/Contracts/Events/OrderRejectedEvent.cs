namespace Warehouse.Common.Contracts.Events
{
    public class OrderRejectedEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
    }
}
