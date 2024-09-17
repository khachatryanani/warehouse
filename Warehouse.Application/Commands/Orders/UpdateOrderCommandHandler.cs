using MediatR;
using Warehouse.Domain.Abstractions;

namespace Warehouse.Application.Commands.Orders
{
    public class UpdateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await orderRepository.UpdateAsync(request.Id, request.Order, cancellationToken);
        }
    }
}
