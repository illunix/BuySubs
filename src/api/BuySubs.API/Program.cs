using BuySubs.API.Extensions;
using BuySubs.API.Filters;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Mappings;
using BuySubs.Common.Options;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Riok.Mapperly.Abstractions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["jwt:secretKey"]!));

builder.Services
    .AddDbContext<InternalDbContext>(q => q.UseNpgsql(configuration["dbConnectionString"]))
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddMediatR(
        q => q.AsScoped(),
        typeof(SignUpCommand)
    )
    .AddMappers()
    .AddDefaultAWSOptions(configuration.GetAWSOptions())
#if RELEASE
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi);
#endif
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .Configure<JwtOptions>(q =>
    {
        q.Issuer = configuration["jwt:issuer"];
        q.Audience = configuration["jwt:audience"];
        q.SigningCredentials = new SigningCredentials(
            signingKey, 
            SecurityAlgorithms.HmacSha256
        );
    })
    .AddAuthorization()
    .AddAuthentication(q =>
    {
        q.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        q.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(q =>
    {
        q.ClaimsIssuer = configuration["jwt:issuer"];
        q.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["jwt:issuer"],

            ValidateAudience = true,
            ValidAudience = configuration["jwt:audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        q.SaveToken = true;

        q.Events = new()
        {
            OnAuthenticationFailed = ctx =>
            {
                if (ctx.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    ctx.Response.Headers.Add(
                        "Token-Expired",
                        "true"
                    );
                }

                return Task.CompletedTask;
            }
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .UseAuthentication()
    .UseAuthorization();

app.MapEndpoints();

app.UseExceptionHandler(q =>
{
    q.Run(async ctx =>
    {
        var exceptionHandlerPathFeature = ctx.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature!.Error;

        var (
            statusCode,
            errorCode
        ) = exception.ParseException();

        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = (int)statusCode;

        await ctx.Response.WriteAsJsonAsync(new
        {
            error = exception.Message,
            code = errorCode
        });
    });
});

app.Run();