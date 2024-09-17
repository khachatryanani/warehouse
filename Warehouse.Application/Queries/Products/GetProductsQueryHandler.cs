using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Products
{
    public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, GetProductsResponse>
    {
        public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var data = await productRepository.GetAsync(cancellationToken);
            return new GetProductsResponse(data);
        }
    }
}
