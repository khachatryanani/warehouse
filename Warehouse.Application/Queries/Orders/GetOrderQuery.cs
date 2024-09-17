using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Orders
{
    public class GetOrderQuery(int id): IRequest<GetOrderQueryResponse>
    {
        public int Id { get; set; } = id;
    }

    public class GetOrderQueryResponse(Order order) 
    {
        public Order Order { get; set; } = order;
    }
}
