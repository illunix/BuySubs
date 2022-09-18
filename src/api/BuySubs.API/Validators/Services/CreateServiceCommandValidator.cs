using BuySubs.BLL.Commands.Services;
using FluentValidation;

namespace BuySubs.API.Validators.Services;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(q => q.SiteId)
            .NotEmpty();

        RuleFor(q => q.Name)
            .NotEmpty();

        RuleFor(q => q.Price)
            .NotEmpty();
    }
}