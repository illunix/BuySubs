using BuySubs.API.Filters;
using BuySubs.BLL.Interfaces;
using MediatR;

namespace BuySubs.API.Extensions;

public static class ApiExtensions
{
    public static WebApplication Post<TRequest>(
        this WebApplication app,
        string template
    ) where TRequest : IHttpRequest
    {
        app.MapPost(
            template,
            async (
                TRequest req,
                IMediator mediator
            )
                => await mediator.Send(req)
        )
            .AddEndpointFilter<ValidationFilter<TRequest>>();

        return app;
    }
}