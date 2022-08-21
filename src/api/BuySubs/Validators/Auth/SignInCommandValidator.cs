using BuySubs.BLL.Commands.Auth;
using FluentValidation;

namespace BuySubs.API.Validators.Auth;

public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(q => q.Password)
            .NotEmpty();
    }
}