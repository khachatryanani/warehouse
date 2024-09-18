using MediatR;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;

namespace Warehouse.Application.Queries.Categories
{
    public class GetCategoryQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, GetCategoryResponse>
    {
        public async Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            return data is null ? throw new DataNotFoundException(ErrorMessages.DataNotFound)
                                : new GetCategoryResponse(data);
        }
    }
}
