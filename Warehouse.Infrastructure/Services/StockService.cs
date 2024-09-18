using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Resources;

namespace Warehouse.Infrastructure.Services
{
    internal class StockService(IProductRepository productRepository, ICategoryRepository categoryRepository): IStockService
    {
        public async Task<StockState> GetStockStateByProductIdAsync(int productId, CancellationToken cancellationToken = default) 
        {
            var product  = await productRepository.GetByIdAsync(productId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Product is unavailable."));

            var category = await categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Category is unavailable."));
            if (product.SockItemsCount <= category.OutOfStockThreshold)
            {
                return StockState.OutOfStock;
            }
            else if (product.SockItemsCount <= category.LowStockThreshold)
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
            await productRepository.UpdateAsync(productId, product, cancellationToken);

            return product.SockItemsCount;
        }

        public async Task<int> AddStockItemsByProductIdAsync(int productId, int count, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(productId, cancellationToken)
                                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.InvalidOperation, "Product is unavailable."));

            product.SockItemsCount += count;
            await productRepository.UpdateAsync(productId, product, cancellationToken);

            return product.SockItemsCount;
        }
    }
}
