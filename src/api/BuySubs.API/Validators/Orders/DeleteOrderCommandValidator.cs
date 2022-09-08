using BuySubs.BLL.Commands.Orders;
using FluentValidation;

namespace BuySubs.API.Validators.Orders;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}