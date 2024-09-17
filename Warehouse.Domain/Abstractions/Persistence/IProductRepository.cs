using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task CreateAsync(Product data, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, Product data, CancellationToken cancellationToken = default);
    }
}
