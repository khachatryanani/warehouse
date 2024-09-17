using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product data, CancellationToken cancellationToken = default);
    }
}
