using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Products
{
    internal class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await productRepository.CreateAsync(request.Product, cancellationToken);
        }
    }
}
