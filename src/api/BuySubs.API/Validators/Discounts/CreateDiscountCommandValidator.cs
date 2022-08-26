using BuySubs.BLL.Commands.Discounts;
using FluentValidation;

namespace BuySubs.API.Validators.Discounts;

public sealed class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();

        RuleFor(q => q.Value)
            .GreaterThan(0)
            .LessThan(100)
            .NotEmpty();

        RuleFor(q => q.IsActive)
            .NotNull();
    }
}
