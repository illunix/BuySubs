using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Auth;
using BuySubs.BLL.Interfaces;
using BuySubs.Common.Options;
using BuySubs.Common.Security;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuySubs.BLL.CommandHandlers;

public sealed partial class AuthCommandHandlers :
    IHttpRequestHandler<SignInCommand>,
    IHttpRequestHandler<SignUpCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly IOptions<JwtOptions> _jwtOptions;

    [HttpPost("auth/sign-in")]
    public async Task<IResult> Handle(
        SignInCommand req,
        CancellationToken ct
    )
    {
        var user = await _ctx.Users
            .Where(q => q.Email == req.Email)
            .Select(q => new { q.Password, q.Salt })
            .FirstOrDefaultAsync();
        if (user is null)
            throw new NotFoundException(nameof(User));

        if (!SecurityHelper.ValidatePassword(
            req.Password,
            user.Password!,
            user.Salt!
        ))
            throw new InvalidCredentialsException();

        return Results.Ok(new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    issuer: _jwtOptions.Value.Issuer,
                    audience: _jwtOptions.Value.Audience,
                    claims: new Claim[] {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            req.Email!
                        ),
                        new Claim(
                            JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()   
                        )
                    },
                    notBefore: _jwtOptions.Value.NotBefore,
                    expires: _jwtOptions.Value.Expiration,
                    signingCredentials: _jwtOptions.Value.SigningCredentials
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
        if (await _ctx.Users.AnyAsync(q => q.Email == req.Email))
            throw new UserWithThisEmailAlreadyExistException();

        var salt = SecurityHelper.GetRandomBytes();

        _ctx.Add(new User
        {
            Email = req.Email,
            Password = SecurityHelper.HashPassword(
                req.Password,
                salt
            ),
            Salt = Convert.ToBase64String(salt)
        });

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }
}