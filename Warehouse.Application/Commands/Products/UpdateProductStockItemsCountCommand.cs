using MediatR;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductStockItemsCountCommand(int id, int stockItemsCount) : IRequest
    {
        public int StockItesCount { get; init; } = stockItemsCount;

        public int Id { get; init; } = id;
    }
}
