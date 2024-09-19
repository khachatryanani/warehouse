using FluentValidation;
using Warehouse.Api.Models.RequestDtos;

namespace Warehouse.Api.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.CategoryId).GreaterThan(default(int));
            RuleFor(x => x.SockItemsCount).GreaterThanOrEqualTo(default(int));
        }
    }
}
