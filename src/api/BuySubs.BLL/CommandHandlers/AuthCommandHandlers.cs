using BuySubs.BLL.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class AuthCommandHandlers :
    IRequestHandler<SignUpCommand, IResult>
{
    public async Task<IResult> Handle(
        SignUpCommand req,
        CancellationToken ct
    )
    {
        return await Task.FromResult(Results.Ok());
    }
}