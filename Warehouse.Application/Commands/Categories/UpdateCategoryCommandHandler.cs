using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Categories
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            await categoryRepository.UpdateAsync(request.Id, request.Category, cancellationToken);
        }
    }
}
