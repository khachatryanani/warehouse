using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Orders
{
    public class CreateOrderCommand(Order order): IRequest
    {
        public Order Order { get; set; } = order;
    }
}
