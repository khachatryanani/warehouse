using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Products
{
    public class CreateProductCommand(Product product): IRequest
    {
        public Product Product { get; set; } = product;
    }
}
