using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Queries.Products
{
    public class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var data = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            return data is null ? throw new DataNotFoundException(ErrorMessages.DataNotFound)
                                : new GetProductResponse(data);
        }
    }
}
