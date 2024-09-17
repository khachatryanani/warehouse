using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductCommand(Product product, int id) : IRequest
    {
        public Product Product { get; init; } = product;

        public int Id { get; init; } = id;
    }
}
