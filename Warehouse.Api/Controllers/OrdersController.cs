using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Application.Commands.Orders;
using Warehouse.Application.Commands.Products;
using Warehouse.Application.Queries.Orders;
using Warehouse.Application.Queries.Products;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Controllers
{
    public class OrdersController(IMapper mapper, IMediator sender) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] OrderRequestDto request, CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(mapper.Map<Order>(request), request.ReserveMode);
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAsync([FromQuery] int? userId, CancellationToken cancellationToken)
        {
            GetOrdersQueryResponse queryResult;
            if (userId.HasValue)
            {
                var query = new GetOrdersByUserIdQuery(userId.Value);
                queryResult = await sender.Send(query, cancellationToken: cancellationToken);
            }
            else 
            {
                var query = new GetOrdersQuery();
                queryResult = await sender.Send(query, cancellationToken: cancellationToken);
            }
            
            var response = mapper.Map<IEnumerable<OrderResponseDto>>(queryResult.Orders);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetOrderQuery(id);
            var queryResult = await sender.Send(query, cancellationToken: cancellationToken);

            var response = mapper.Map<OrderResponseDto>(queryResult.Order);
            return Ok(response);
        }
    }
}
