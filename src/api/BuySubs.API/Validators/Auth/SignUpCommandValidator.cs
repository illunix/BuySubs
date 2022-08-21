using BuySubs.BLL.Commands.Auth;
using FluentValidation;

namespace BuySubs.API.Validators.Auth;

public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(q => q.Password)
            .NotEmpty();

        RuleFor(q => q.ConfirmPassword)
            .NotEmpty()
            .Equal(q => q.Password);
    }
}