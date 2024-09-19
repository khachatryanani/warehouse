using FluentValidation;
using Warehouse.Api.Models.RequestDtos;

namespace Warehouse.Api.Validators
{
    public class UpdateProductValidator: CustomeAbstractValidator<ProductRequestDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.CategoryId).GreaterThan(default(int));
        }
    }
}
