using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Products
{
    public class GetProductQuery(int id) : IRequest<GetProductResponse>
    {
        public int Id { get; init; } = id;
    }

    public class GetProductResponse(Product product)
    {
        public Product Product { get; init; } = product;
    }
}
