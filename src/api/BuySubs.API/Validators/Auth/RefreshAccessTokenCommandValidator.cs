using BuySubs.BLL.Commands.Auth;
using FluentValidation;

namespace BuySubs.API.Validators.Auth;

public sealed class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
{
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(q => q.RefreshToken)
            .NotEmpty();
    }
}