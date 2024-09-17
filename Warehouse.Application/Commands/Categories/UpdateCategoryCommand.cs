using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Categories
{
    public class UpdateCategoryCommand(int id, Category category) : IRequest
    {
        public Category Category { get; init; } = category;

        public int Id { get; init; } = id;
    }
}
