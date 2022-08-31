using BuySubs.BLL.Commands.Services;
using FluentValidation;

namespace BuySubs.API.Validators.Services;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
        => RuleFor(q => q.Id)
            .NotEmpty();
}