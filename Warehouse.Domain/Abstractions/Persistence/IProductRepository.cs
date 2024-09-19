using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(Product entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, Product entity, CancellationToken cancellationToken = default);
        Task UpdateStockItemsCountAsync(int id, int stockItems, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
