using BuySubs.BLL.Commands.Sites;
using FluentValidation;

namespace BuySubs.API.Validators.Sites;

public sealed class DeleteSiteCommandValidator : AbstractValidator<DeleteSiteCommand>
{
    public DeleteSiteCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}