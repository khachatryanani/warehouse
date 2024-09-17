using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<GetProductsResponse>
    {
    }

    public class GetProductsResponse(IEnumerable<Product> products)
    {
        public IEnumerable<Product> Products { get; init; } = products;
    }
}
