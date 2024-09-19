using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Abstractions.Services
{
    public interface IStockService
    {
        Task<StockState> GetStockStateByProductIdAsync(int productId, int requestedCount, CancellationToken cancellationToken = default);
        Task<int> TakeStockItemsByProductIdAsync(int productId, int count, CancellationToken cancellationToken = default);
        Task<int> AddStockItemsByProductIdAsync(int productId, int count, CancellationToken cancellationToken = default);
        Task UpdateStockItemsByProductIdAsync(int productId, int stockItemsCount, CancellationToken cancellationToken = default);
    }
}
