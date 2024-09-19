using MassTransit;
using MediatR;
using Warehouse.Common.Contracts.Events;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Events;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Commands.Orders
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IRequestClient<OrderCreatedEvent> requestClient) : IRequestHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await CreateInitialOrderAsync(request, cancellationToken);
            await ProceedOrderAsync(request, cancellationToken);
        }

        private async Task CreateInitialOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            await ValidateProduct(request.Order.ProductId, cancellationToken);

            request.Order.Status = OrderStatus.Created;
            request.Order.Id = await orderRepository.CreateAsync(request.Order, cancellationToken);
        }

        private async Task ProceedOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await requestClient.GetResponse<OrderCreatedEventResponse>(new OrderCreatedEvent()
            {
                CorrelationId = Guid.NewGuid(),
                OrderId = request.Order.Id,
                Mode = request.Mode
            }, cancellationToken: cancellationToken);

            if (!string.IsNullOrEmpty(result.Message?.ErrorMessage)) 
            {
                if (result.Message.Status is OrderStatus.Rejected)
                {
                    throw new OutOfStockException(result.Message.ErrorMessage);
                }
                else 
                {
                    throw new Domain.Exceptions.InvalidOperationException(result.Message.ErrorMessage);
                }
            }
        }

        private async Task ValidateProduct(int productId, CancellationToken cancellationToken)
        {
            if (!await productRepository.ExistsAsync(productId, cancellationToken))
            {
                throw new InvalidStateException(ErrorMessages.InvalidProduct);
            }
        }
    }
}
