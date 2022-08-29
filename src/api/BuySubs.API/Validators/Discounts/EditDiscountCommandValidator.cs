using BuySubs.BLL.Commands.Discounts;
using FluentValidation;

namespace BuySubs.API.Validators.Discounts;

public sealed class EditDiscountCommandValidator : AbstractValidator<EditDiscountCommand>
{
    public EditDiscountCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();

        RuleFor(q => q.Value)
            .NotEmpty();

        RuleFor(q => q.IsActive)
            .NotNull();
    }
}