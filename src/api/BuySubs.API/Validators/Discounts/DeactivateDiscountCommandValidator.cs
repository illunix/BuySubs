using BuySubs.BLL.Commands.Discounts;
using FluentValidation;

namespace BuySubs.API.Validators.Discounts;

public sealed class DeactivateDiscountCommandValidator : AbstractValidator<DeactivateDiscountCommand>
{
    public DeactivateDiscountCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();
    }
}