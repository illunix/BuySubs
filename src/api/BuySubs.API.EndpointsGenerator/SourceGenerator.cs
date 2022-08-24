﻿using DispatchEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BuySubs.API.EndpointsGenerator;

[Generator]
internal class SourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // Debugger.Launch();

        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver)
        {
            return;
        }

        var methods = GetMethods(
            context,
            syntaxReceiver
        );

        var extensions = new StringBuilder();

        extensions.AppendLine(
@$"using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BuySubs.API.Filters;

namespace BuySubs.API.Extensions;

public static class ApiExtensions
{{
    public static WebApplication MapEndpoints(this WebApplication app)
    {{
        {GenerateEndpoints(methods)}
        return app;
    }}
}}
"
        );

        context.AddSource(
            "BuySubs.API.Extensions.g.cs",
            SourceText.From(
                extensions.ToString(),
                Encoding.UTF8
            )
        );

        var filters = new StringBuilder();

        filters.AppendLine(
@"using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BuySubs.API.Filters;

internal class ValidationFilter<T> : IEndpointFilter
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
            return Results.BadRequest(""The parameter is invalid."");
        }

        var validationResult = await _validator.ValidateAsync((T)parameter);
        if (!validationResult.IsValid)
        {
            var result = new
            {
                Message = ""Validation errors"",
                Errors = validationResult.Errors.Select(q => q.ErrorMessage)
            };

            return Results.BadRequest(result);
        }

        return await next(ctx);
    }
}"
        );
        context.AddSource(
            "BuySubs.API.Filters.g.cs",
            SourceText.From(
                filters.ToString(),
                Encoding.UTF8
            )
        );
    }


    private static string GenerateEndpoints(IEnumerable<IMethodSymbol> methods)
    {
        var sb = new StringBuilder();

        foreach (var method in methods)
        {
            var httpAttr = method.GetAttributes().FirstOrDefault()!;
            var httpMethod = httpAttr.AttributeClass?.Name
                .Replace(
                    "Http",
                    ""
                )
                .Replace(
                    "Attribute",
                    ""
                );
            var requestType = method.Parameters.FirstOrDefault()!.Type;

            var route = httpAttr.ConstructorArguments.FirstOrDefault().Value;

            sb.AppendLine(
@$"app.Map{httpMethod}(
            ""{route}"",
            async (
                {requestType} req,
                IMediator mediator
            )
                => await mediator.Send(req)
            )
                .AddEndpointFilter<ValidationFilter<{requestType}>>();"
            );
        }

        return sb.ToString();
    }

    private static IEnumerable<IMethodSymbol> GetMethods(
       GeneratorExecutionContext context,
       SyntaxReceiver receiver
    )
    {
        var compilation = context.Compilation;

        foreach (var @method in receiver.CandidateMethods)
        {
            var model = compilation.GetSemanticModel(@method.SyntaxTree);
            var methodSymbol = (IMethodSymbol)model.GetDeclaredSymbol(@method)!;
            if (methodSymbol is null)
            {
                break;
            }

            if (methodSymbol.GetAttributes()
                .Select(q => q.AttributeClass?.Name)
                .Any(q => 
                    q == nameof(HttpGetAttribute) ||
                    q == nameof(HttpPostAttribute) ||
                    q == nameof(HttpPutAttribute) ||
                    q == nameof(HttpDeleteAttribute)
                )
            )
            {
                yield return methodSymbol;
            }
        }
    }
}