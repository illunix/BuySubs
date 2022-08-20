using FluentValidation;

namespace BuySubs.API.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public virtual async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext ctx,
        EndpointFilterDelegate next
    )
    {
        var parameter = ctx.GetArgument<T>(0);
        if (parameter is null)
        {
            return Results.BadRequest("The parameter is invalid.");
        }

        var validationResult = await _validator.ValidateAsync((T)parameter);
        if (!validationResult.IsValid)
        {
            var result = new
            {
                Message = "Validation errors",
                Errors = validationResult.Errors.Select(q => q.ErrorMessage)
            };

            return Results.BadRequest(result);
        }

        return await next(ctx);
    }
}