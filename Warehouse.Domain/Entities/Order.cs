using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemsCount { get; set; }
        public OrderStatus Status { get; set; } 
    }
}
