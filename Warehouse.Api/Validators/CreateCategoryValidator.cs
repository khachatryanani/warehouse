using FluentValidation;
using Warehouse.Api.Models.RequestDtos;

namespace Warehouse.Api.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryRequestDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.OutOfStockThreshold).GreaterThanOrEqualTo(default(int));
            RuleFor(x => x.LowStockThreshold).GreaterThan(x => x.OutOfStockThreshold);
        }
    }
}
