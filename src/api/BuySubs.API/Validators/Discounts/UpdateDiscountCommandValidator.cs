using BuySubs.BLL.Commands.Discounts;
using FluentValidation;

namespace BuySubs.API.Validators.Discounts;

public sealed class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();

        RuleFor(q => q.Name)
            .NotEmpty();

        RuleFor(q => q.Value)
            .NotEmpty();

        RuleFor(q => q.IsActive)
            .NotNull();
    }
}