using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Auth;
using BuySubs.Common.DTO.Auth;
using BuySubs.Common.Options;
using BuySubs.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    private readonly JwtOptions _jwtOptions;

    public AuthCommandHandlers(
        IDynamoDBContext ctx,
        IOptions<JwtOptions> jwtOptions
    )
    {
        _ctx = ctx;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("auth/sign-in")]
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

        return Results.Ok(new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: new Claim[] {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            user.Email!
                        ),
                        new Claim(
                            JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()   
                        )
                    },
                    notBefore: _jwtOptions.NotBefore,
                    expires: _jwtOptions.Expiration,
                    signingCredentials: _jwtOptions.SigningCredentials
                )
            ),
            refresh_token = Convert.ToBase64String(SecurityHelper.GetRandomBytes())
        });
    }

    [HttpPost("auth/sign-up")]
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