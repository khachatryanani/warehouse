using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Categories
{
    internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand>
    {
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            ValidateThresholds(request.Category);
            await categoryRepository.CreateAsync(request.Category, cancellationToken);
        }

        private void ValidateThresholds(Category category) 
        {
            if (category.OutOfStockThreshold < 0 || category.LowStockThreshold < 0 || category.LowStockThreshold <= category.OutOfStockThreshold) 
            {
                throw new InvalidStateException(ErrorMessages.InvalidThresholds);
            }
        }
    }
}
