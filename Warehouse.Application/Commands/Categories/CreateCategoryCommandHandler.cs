using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Categories
{
    internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand>
    {
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryRepository.CreateAsync(request.Category, cancellationToken);
        }
    }
}
