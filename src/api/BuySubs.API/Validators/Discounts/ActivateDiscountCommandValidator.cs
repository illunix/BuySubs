using BuySubs.BLL.Commands.Discounts;
using FluentValidation;

namespace BuySubs.API.Validators.Discounts;

public sealed class ActivateDiscountCommandValidator : AbstractValidator<ActivateDiscountCommand>
{
    public ActivateDiscountCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();
    }
}