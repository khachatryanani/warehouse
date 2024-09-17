using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Orders
{
    public class GetOrderQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
    {
        public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var data = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
            return new GetOrderQueryResponse(data);
        }
    }
}
