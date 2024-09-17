using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Orders
{
    public class GetOrdersQuery : IRequest<GetOrdersQueryResponse>
    {
    }
    
    public class GetOrdersByUserIdQuery(int userId) : IRequest<GetOrdersQueryResponse>
    {
        public int UserId { get; set; } = userId;
    }

    public class GetOrdersQueryResponse(IEnumerable<Order> orders)
    {
        public IEnumerable<Order> Orders { get; set; } = orders;
    }
}
