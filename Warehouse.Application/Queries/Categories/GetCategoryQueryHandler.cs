using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Categories
{
    public class GetCategoryQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, GetCategoryResponse>
    {
        public async Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            return new GetCategoryResponse(data);
        }
    }
}
