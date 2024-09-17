using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Categories
{
    public class CreateCategoryCommand(Category category): IRequest
    {
        public Category Category { get; set; } = category;
    }
}
