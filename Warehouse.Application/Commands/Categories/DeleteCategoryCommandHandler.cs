using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Categories
{
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
