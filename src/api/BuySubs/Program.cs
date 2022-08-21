using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BuySubs.API.Extensions;
using BuySubs.BLL.Commands.Auth;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddMediatR(
        q => q.AsScoped(),
        typeof(SignUpCommand)
    )
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDefaultAWSOptions(configuration.GetAWSOptions())
#if RELEASE
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi);
#endif
    .AddAWSService<IAmazonDynamoDB>()
    .AddScoped<IDynamoDBContext, DynamoDBContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Auth
app
    .Post<SignUpCommand>("auth/sign-up");
#endregion

app.Run();