using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Application.Commands.Categories;
using Warehouse.Application.Queries.Categories;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Controllers
{
    public class CategoriesController(IMapper mapper, IMediator sender) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CategoryRequestDto request, CancellationToken cancellationToken)
        {
            var command = new CreateCategoryCommand(mapper.Map<Category>(request));
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAsync(CancellationToken cancellationToken)
        {
            var query = new GetCategoriesQuery();
            var queryResult = await sender.Send(query, cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<CategoryResponseDto>>(queryResult.Categories);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetCategoryQuery(id);
            var queryResult = await sender.Send(query, cancellationToken: cancellationToken);

            var response = mapper.Map<CategoryResponseDto>(queryResult.Category);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] CategoryRequestDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryCommand(id, mapper.Map<Category>(request));
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            await sender.Send(command, cancellationToken: cancellationToken);

            return Ok();
        }

    }
}
