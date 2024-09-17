using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Orders
{
    public class GetOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersQuery, GetOrdersQueryResponse>,
                                                                           IRequestHandler<GetOrdersByUserIdQuery, GetOrdersQueryResponse>
    {
        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var data = await orderRepository.GetAsync(cancellationToken);
            return new GetOrdersQueryResponse(data);
        }

        public async Task<GetOrdersQueryResponse> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var data = await orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            return new GetOrdersQueryResponse(data);
        }
    }
}
