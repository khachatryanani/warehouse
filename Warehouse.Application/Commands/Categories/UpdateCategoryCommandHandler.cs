using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Categories
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await ValidateCategory(request.Category.Id, cancellationToken);
            ValidateThresholds(request.Category);

            await categoryRepository.UpdateAsync(request.Id, request.Category, cancellationToken);
        }

        private async Task ValidateCategory(int categoryId, CancellationToken cancellationToken)
        {
            if (!await categoryRepository.ExistsAsync(categoryId, cancellationToken))
            {
                throw new DataNotFoundException(ErrorMessages.DataNotFound);
            }
        }

        private static void ValidateThresholds(Category category)
        {
            if (category.OutOfStockThreshold < 0 || category.LowStockThreshold < 0 || category.LowStockThreshold <= category.OutOfStockThreshold)
            {
                throw new InvalidStateException(ErrorMessages.InvalidThresholds);
            }
        }
    }
}
