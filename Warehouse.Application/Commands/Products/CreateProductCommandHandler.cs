using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Products
{
    internal class CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await ValidateCategory(request.Product.CategoryId, cancellationToken);
            await productRepository.CreateAsync(request.Product, cancellationToken);
        }

        private async Task ValidateCategory(int categoryId, CancellationToken cancellationToken) 
        {
            if (!await categoryRepository.ExistsAsync(categoryId, cancellationToken)) 
            {
                throw new InvalidStateException(ErrorMessages.InvalidCategory);
            }
        }
    }
}
