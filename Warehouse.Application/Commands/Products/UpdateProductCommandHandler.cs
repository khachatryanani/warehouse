using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            await productRepository.UpdateAsync(request.Id, request.Product, cancellationToken);
        }
    }
}
