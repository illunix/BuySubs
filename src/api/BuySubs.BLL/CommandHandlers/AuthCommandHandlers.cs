using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Auth;
using BuySubs.Common.DTO.Auth;
using BuySubs.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class AuthCommandHandlers :
    IRequestHandler<SignInCommand, IResult>,
    IRequestHandler<SignUpCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public AuthCommandHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IResult> Handle(
        SignInCommand req,
        CancellationToken ct
    )
    {
        var user = await _ctx.LoadAsync<UserSignInDTO>(req.Email);
        if (user is null)
        {
            throw new NotFoundException(nameof(UserSignInDTO));
        }

        if (!SecurityHelper.ValidatePassword(
            req.Password,
            user.Password!,
            user.Salt!
        ))
        {
            throw new InvalidCredentialsException();
        }

        return Results.Ok(new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                issuer: "",
                audience: "",
                claims: new Claim[] {
                    // new Claim(ClaimTypes.Role, "admin"),
                    new Claim(
                        ClaimTypes.Name,
                        "桂素伟"
                    )
                },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(500000),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg")),
                    SecurityAlgorithms.HmacSha256
                ))
            )
        );
    }

    // [HttpPost("auth/sign-up")]
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

        await _ctx.SaveAsync(new UserSignUpDTO
        {
            Email = req.Email,
            Password = SecurityHelper.HashPassword(
                req.Password,
                salt
            ),
            Salt = Convert.ToBase64String(salt)
    });

        return Results.Ok();
    }
}