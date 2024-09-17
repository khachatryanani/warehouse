using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Categories
{
    public class GetCategoriesQuery : IRequest<GetCategoriesResonse>
    {
    }

    public class GetCategoriesResonse(IEnumerable<Category> categories)
    {
        public IEnumerable<Category> Categories { get; init; } = categories;
    }
}
