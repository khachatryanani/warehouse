using MassTransit;
using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Events;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProductStockItemsCountCommandHandler(IProductRepository productRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<UpdateProductStockItemsCountCommand>
    {
        public async Task Handle(UpdateProductStockItemsCountCommand request, CancellationToken cancellationToken)
        {
            await ValidateProduct(request.Id, cancellationToken);

            await productRepository.UpdateStockItemsCountAsync(request.Id, request.StockItesCount, cancellationToken);
            await publishEndpoint.Publish(new StockChangedEvent 
            {
                ProductId = request.Id,
                StockItemsCount = request.StockItesCount,
            });
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
