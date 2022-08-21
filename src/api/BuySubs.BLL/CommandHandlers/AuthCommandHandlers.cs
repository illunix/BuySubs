using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Exceptions.Auth;
using BuySubs.Common.DTO.Auth.SignUpCommand;
using BuySubs.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class AuthCommandHandlers :
    IRequestHandler<SignUpCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public AuthCommandHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IResult> Handle(
        SignUpCommand req,
        CancellationToken ct
    )
    {
        if ((await _ctx.LoadAsync<UserWithThisEmailAlreadyExistDTO>(req.Email)) is not null)
        {
            throw new UserWithThisEmailAlreadyExistException();
        }

        var salt = SecurityHelper.GetRandomBytes();

        await _ctx.SaveAsync(new UserDTO
        {
            Email = req.Email,
            Password = SecurityHelper.HashPassword(
                req.Password,
                salt
            ),
            Salt = salt
        });

        return Results.Ok();
    }
}