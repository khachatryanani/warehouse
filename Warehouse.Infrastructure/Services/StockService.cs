using MassTransit;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Events;
using Warehouse.Domain.Resources;

namespace Warehouse.Infrastructure.Services
{
    internal class StockService(IProductRepository productRepository, ICategoryRepository categoryRepository, IPublishEndpoint publishEndpoint) : IStockService
    {
        public async Task<StockState> GetStockStateByProductIdAsync(int productId, int requestedCount, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(productId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Product is unavailable."));

            var category = await categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Category is unavailable."));

            var remainingStock = product.SockItemsCount - requestedCount;
            if (remainingStock <= category.OutOfStockThreshold)
            {
                return StockState.OutOfStock;
            }
            else if (remainingStock <= category.LowStockThreshold)
            {
                return StockState.LowStock;
            }
            else
            {
                return StockState.Available;
            }
        }

        public async Task<int> TakeStockItemsByProductIdAsync(int productId, int count, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(productId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Product is unavailable."));

            if (count > product.SockItemsCount)
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Insufficient number of stock items."));
            }

            product.SockItemsCount -= count;
            await productRepository.UpdateStockItemsCountAsync(productId, product.SockItemsCount, cancellationToken);

            await NotifyStockChangedAsync(productId, product.SockItemsCount, cancellationToken);

            return product.SockItemsCount;
        }

        public async Task<int> AddStockItemsByProductIdAsync(int productId, int count, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(productId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Product is unavailable."));

            product.SockItemsCount += count;
            await productRepository.UpdateStockItemsCountAsync(productId, product.SockItemsCount, cancellationToken);

            await NotifyStockChangedAsync(productId, product.SockItemsCount, cancellationToken);

            return product.SockItemsCount;
        }

        public async Task UpdateStockItemsByProductIdAsync(int productId, int stockItemsCount, CancellationToken cancellationToken = default)
        {
            await productRepository.UpdateStockItemsCountAsync(productId, stockItemsCount, cancellationToken);
            await NotifyStockChangedAsync(productId, stockItemsCount, cancellationToken);
        }

        private Task NotifyStockChangedAsync(int productId, int stockItemsCount, CancellationToken cancellationToken)
        => publishEndpoint.Publish(new StockChangedEvent
        {
            ProductId = productId,
            StockItemsCount = stockItemsCount,
        }, cancellationToken);

    }
}
