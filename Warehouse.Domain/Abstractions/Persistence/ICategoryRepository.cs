using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Abstractions
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task CreateAsync(Category entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, Category entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
