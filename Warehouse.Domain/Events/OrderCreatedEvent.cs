using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public ReserveMode Mode { get; set; }
    }
}
