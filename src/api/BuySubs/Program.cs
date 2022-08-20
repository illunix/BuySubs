using BuySubs.API.Extensions;
using BuySubs.API.Filters;
using BuySubs.BLL.Commands.Auth;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddMediatR(
        q => q.AsScoped(),
        typeof(SignUpCommand)
    )
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

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