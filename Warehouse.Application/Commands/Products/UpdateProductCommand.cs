using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductCommand(int id, Product product) : IRequest
    {
        public Product Product { get; init; } = product;

        public int Id { get; init; } = id;
    }
}
