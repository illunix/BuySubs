using BuySubs.API.Extensions;
using BuySubs.API.Filters;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Services;
using BuySubs.Common.Options;
using BuySubs.DAL.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["jwt:secretKey"]!));

services.AddMvcCore(q =>
{
    q.Filters.Add(typeof(CustomExceptionFilterAttribute));
}).Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<InternalDbContext>(q => q.UseNpgsql(configuration["dbConnectionString"]))
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddMediatR(
        q => q.AsScoped(),
        typeof(SignUpCommand)
    )
    .AddDefaultAWSOptions(configuration.GetAWSOptions())
#if RELEASE
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi);
#endif
    .AddEnyimMemcached()
    .Configure<JwtOptions>(q =>
    {
        q.Issuer = configuration["jwt:issuer"];
        q.Audience = configuration["jwt:audience"];
        q.SigningCredentials = new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha256
        );
    })
    .AddSingleton<IJwtHandler, JwtHandler>()
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

app.UseEnyimMemcached();

app.MapEndpoints();

app.Run();