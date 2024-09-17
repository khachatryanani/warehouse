using MediatR;

namespace Warehouse.Application.Commands.Categories
{
    public class DeleteCategoryCommand(int id): IRequest
    {
        public int Id { get; set; } = id;
    }
}
