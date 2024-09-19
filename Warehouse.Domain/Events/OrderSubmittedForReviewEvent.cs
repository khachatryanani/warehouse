using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Events
{
    public class OrderSubmittedForReviewEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public ReserveMode Mode { get; set; }
    }
}
