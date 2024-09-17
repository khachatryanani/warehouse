using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Queries.Categories
{
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, GetCategoriesResonse>
    {
        public async Task<GetCategoriesResonse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var data = await categoryRepository.GetAsync(cancellationToken);
            return new GetCategoriesResonse(data);
        }
    }
}
