using BuySubs.BLL.Commands.Sites;
using FluentValidation;

namespace BuySubs.API.Validators.Sites;

public sealed class DeactivateSiteCommandValidator : AbstractValidator<DeactivateSiteCommand>
{
    public DeactivateSiteCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty();
    }
}