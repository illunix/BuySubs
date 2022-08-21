using BuySubs.API.Filters;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuySubs.API.Extensions;

internal static class ApiExtensions
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

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var httpEndpoints = typeof(SignUpCommand).Assembly.GetTypes()
            .SelectMany(q => q.GetMethods())
            .Where(q => q.GetCustomAttributes(
                typeof(HttpPostAttribute),
               false
            ).Length > 0 
        )
        .ToArray();

        foreach (var httpEndpoint in httpEndpoints)
        {
            foreach (var attributeName in httpEndpoint.CustomAttributes.Select(q => q.AttributeType.Name))
            {
                switch (attributeName)
                {
                    case nameof(HttpGetAttribute):
                        break;
                    case nameof(HttpPostAttribute):
                        var val = httpEndpoint.GetParameters().First().ParameterType;

                        /*

                        app.MapPost(
                            template,
                            async (
                                TRequest req,
                                IMediator mediator
                            )
                            => await mediator.Send(req)
                        )
                            .AddEndpointFilter<ValidationFilter<TRequest>>();
                        */
                        break;
                    case nameof(HttpPutAttribute):
                        break;
                    case nameof(HttpDeleteAttribute):
                        break;
                }
            }
        }

        return app;
    }

    private static TValue GetAttributeValue<TAttribute, TValue>(
        this Type type,
        Func<TAttribute, TValue> valueSelector
    )
        where TAttribute : Attribute
    {
        var att = type.GetCustomAttributes(
            typeof(TAttribute), true
        ).FirstOrDefault() as TAttribute;
        if (att != null)
        {
            return valueSelector(att);
        }

        return default(TValue);
    }
}