using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BuySubs.API.Extensions;
using BuySubs.BLL.Commands.Auth;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

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
    .AddAuthorization()
    .AddAuthentication(q =>
    {
        q.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        q.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        q.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(q =>
         q.TokenValidationParameters = new TokenValidationParameters
         {
             ValidIssuer = configuration["jwt:issuer"],
             ValidAudience = configuration["jwt:issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg")),
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = false,
             ValidateIssuerSigningKey = true
         }
     );

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

#region Auth
app
    .Post<SignInCommand>("auth/sign-in")
    .Post<SignUpCommand>("auth/sign-up");

#endregion

app.Run();