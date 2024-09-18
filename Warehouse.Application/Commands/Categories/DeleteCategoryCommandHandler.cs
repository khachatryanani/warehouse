using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;
using InvalidOperationException = Warehouse.Domain.Exceptions.InvalidOperationException;

namespace Warehouse.Application.Commands.Categories
{
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await ValidateCategory(request.Id, cancellationToken);
            await ValidateCanBeDeleted(request.Id, cancellationToken);

            await categoryRepository.DeleteAsync(request.Id, cancellationToken);
        }

        private async Task ValidateCategory(int categoryId, CancellationToken cancellationToken)
        {
            if (!await categoryRepository.ExistsAsync(categoryId, cancellationToken))
            {
                throw new DataNotFoundException(ErrorMessages.DataNotFound);
            }
        }

        private async Task ValidateCanBeDeleted(int categoryId, CancellationToken cancellationToken)
        {
            if (!await productRepository.ExistByCategoryIdAsync(categoryId, cancellationToken))
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "This Category cannot be deleted,"));
            }
        }
    }
}
