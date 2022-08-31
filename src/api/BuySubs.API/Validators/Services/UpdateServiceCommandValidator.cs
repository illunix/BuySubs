using BuySubs.BLL.Commands.Services;
using FluentValidation;

namespace BuySubs.API.Validators.Services;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();

        RuleFor(q => q.Name)
            .NotEmpty();

        RuleFor(q => q.Price)
            .NotEmpty();
    }
}