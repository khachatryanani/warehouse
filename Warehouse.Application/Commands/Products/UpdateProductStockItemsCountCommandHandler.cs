using MassTransit;
using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Events;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductStockItemsCountCommandHandler(IProductRepository productRepository, IStockService stockService) : IRequestHandler<UpdateProductStockItemsCountCommand>
    {
        public async Task Handle(UpdateProductStockItemsCountCommand request, CancellationToken cancellationToken)
        {
            await ValidateProduct(request.Id, cancellationToken);

            await stockService.UpdateStockItemsByProductIdAsync(request.Id, request.StockItesCount, cancellationToken);
        }

        private async Task ValidateProduct(int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.ExistsAsync(productId, cancellationToken))
            {
                throw new DataNotFoundException(ErrorMessages.DataNotFound);
            }
        }
    }
}
