using BuySubs.BLL.Commands.Orders;
using FluentValidation;

namespace BuySubs.API.Validators.Orders;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();

        RuleFor(q => q.ServiceId)
            .NotEmpty();
    }
}