using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task CreateAsync(Order data, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, Order data, CancellationToken cancellationToken = default);
    }
}
