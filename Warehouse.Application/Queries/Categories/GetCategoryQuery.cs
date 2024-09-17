using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Categories
{
    public class GetCategoryQuery(int id) : IRequest<GetCategoryResponse>
    {
        public int Id { get; init; } = id;
    }

    public class GetCategoryResponse(Category category)
    {
        public Category Category { get; init; } = category;
    }
}
