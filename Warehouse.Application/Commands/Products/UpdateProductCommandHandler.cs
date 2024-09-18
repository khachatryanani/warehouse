using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await ValidateProduct(request.Product.Id, cancellationToken);
            await ValidateCategory(request.Product.CategoryId, cancellationToken);

            await productRepository.UpdateAsync(request.Id, request.Product, cancellationToken);
        }

        private async Task ValidateCategory(int categoryId, CancellationToken cancellationToken)
        {
            if (!await categoryRepository.ExistsAsync(categoryId, cancellationToken))
            {
                throw new InvalidStateException(ErrorMessages.InvalidCategory);
            }
        }

        private async Task ValidateProduct(int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.ExistsAsync(productId, cancellationToken))
            {
                throw new DataNotFoundException(ErrorMessages.DataNotFound);
            }
        }
    }
}
