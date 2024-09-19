using FluentValidation;
using Warehouse.Api.Models.RequestDtos;

namespace Warehouse.Api.Validators
{
    public class UpdateProductStockValidator : CustomeAbstractValidator<UpdateProductStockItemsCountRequestDto>
    {
        public UpdateProductStockValidator()
        {
            RuleFor(x => x.StockItemsCount).GreaterThan(default(int));
        }
    }
}
