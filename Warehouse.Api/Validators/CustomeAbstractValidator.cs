using FluentValidation;
using FluentValidation.Results;

namespace Warehouse.Api.Validators
{
    public class CustomeAbstractValidator<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                RaiseValidationException(context, validationResult);
            }

            return validationResult;
        }
    }
}
