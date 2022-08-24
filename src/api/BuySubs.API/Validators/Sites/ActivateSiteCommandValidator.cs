using BuySubs.BLL.Commands.Sites;
using FluentValidation;

namespace BuySubs.API.Validators.Sites;

public sealed class ActivateSiteCommandValidator : AbstractValidator<ActivateSiteCommand>
{
    public ActivateSiteCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();
    }
}