using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Events
{
    public class OrderCreatedEventResponse
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
