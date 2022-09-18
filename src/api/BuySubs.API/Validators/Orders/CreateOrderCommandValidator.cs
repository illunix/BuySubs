using BuySubs.BLL.Commands.Orders;
using FluentValidation;

namespace BuySubs.API.Validators.Orders;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(q => q.ServiceId)
            .NotEmpty();
    }
}