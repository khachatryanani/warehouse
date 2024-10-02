using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Queries.Orders
{
    public class GetOrderQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
    {
        public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var data = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
          
            return data is null ? throw new DataNotFoundException(ErrorMessages.DataNotFound)
                                : new GetOrderQueryResponse(data);
        }
    }
}
