using BuySubs.BLL.Commands.Sites;
using FluentValidation;

namespace BuySubs.API.Validators.Sites;

public sealed class UpdateSiteCommandValidator : AbstractValidator<UpdateSiteCommand>
{
    public UpdateSiteCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();

        RuleFor(q => q.Name)
            .NotEmpty();
    }
}