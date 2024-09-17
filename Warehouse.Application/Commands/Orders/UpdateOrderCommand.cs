using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Orders
{
    public class UpdateOrderCommand(int id, Order order) : IRequest
    {
        public int Id { get; set; } = id;
        public Order Order { get; set; } = order;
    }
}
