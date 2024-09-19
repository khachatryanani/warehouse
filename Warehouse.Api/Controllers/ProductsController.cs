using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Application.Commands.Products;
using Warehouse.Application.Queries.Products;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Controllers
{
    public class ProductsController(IMapper mapper, IMediator sender) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductRequestDto request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(mapper.Map<Product>(request));
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAsync(CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery();
            var queryResult = await sender.Send(query, cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<ProductResponseDto>>(queryResult.Products);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetProductQuery(id);
            var queryResult = await sender.Send(query, cancellationToken: cancellationToken);

            var response = mapper.Map<ProductResponseDto>(queryResult.Product);
            return Ok(response);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id,[FromBody] ProductRequestDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(id, mapper.Map<Product>(request));
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateStockItemsCountAsync(UpdateProductStockItemsCountRequestDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductStockItemsCountCommand(request.Id, request.StockItemsCount);
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }
    }
}
