using FluentValidation;
using Warehouse.Api.Models.RequestDtos;

namespace Warehouse.Api.Validators
{
    public class CreateOrderValidator : CustomeAbstractValidator<OrderRequestDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(default(int));
            RuleFor(x => x.ProductId).GreaterThan(default(int));
            RuleFor(x => x.ItemsCount).GreaterThan(default(int));
            RuleFor(x => x.ReserveMode).IsInEnum();
        }
    }
}
