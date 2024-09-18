using MediatR;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Orders
{
    public class CreateOrderCommand(Order order, ReserveMode mode): IRequest
    {
        public Order Order { get; set; } = order;
        public ReserveMode Mode { get; set; } = mode;
    }
}
