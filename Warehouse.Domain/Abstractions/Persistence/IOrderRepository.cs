using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(Order entity, CancellationToken cancellationToken = default);
        Task UpdateStatusAsync(int id, OrderStatus status, CancellationToken cancellationToken = default);
    }
}
