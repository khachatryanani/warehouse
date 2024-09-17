using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Orders
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await orderRepository.CreateAsync(request.Order, cancellationToken);
        }
    }
}
