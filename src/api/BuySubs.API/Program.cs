using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BuySubs.API.Extensions;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Commands.Sites;
using BuySubs.Common.Options;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["jwt:secretKey"]!));

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddMediatR(
        q => q.AsScoped(),
        typeof(SignUpCommand)
    )
    .AddDefaultAWSOptions(configuration.GetAWSOptions())
#if RELEASE
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi);
#endif
    .AddAWSService<IAmazonDynamoDB>()
    .AddScoped<IDynamoDBContext, DynamoDBContext>()
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

#region Sites
app
    .Post<CreateSiteCommand>("site");
#endregion

app.Run();