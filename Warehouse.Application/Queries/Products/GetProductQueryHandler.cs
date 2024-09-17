using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Products
{
    public class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var data = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            return new GetProductResponse(data);
        }
    }
}
