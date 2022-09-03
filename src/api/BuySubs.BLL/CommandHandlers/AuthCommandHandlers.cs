using BuySubs.BLL.Commands.Auth;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Auth;
using BuySubs.BLL.Interfaces;
using BuySubs.Common.DTO.Auth;
using BuySubs.Common.Options;
using BuySubs.Common.Security;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class AuthCommandHandlers :
    IHttpRequestHandler<SignInCommand>,
    IHttpRequestHandler<SignUpCommand>,
    IHttpRequestHandler<RefreshAccessTokenCommand>,
    IHttpRequestHandler<RevokeRefreshTokenCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly IDistributedCache _refreshTokensRepo;
    private readonly ITokenService _tokenService;

    public AuthCommandHandlers(
        InternalDbContext ctx,
        IDistributedCache refreshTokensRepo,
        ITokenService tokenService
    )
    {
        _ctx = ctx;
        _refreshTokensRepo = refreshTokensRepo;
        _tokenService = tokenService;
    }

    [HttpPost("auth/sign-in")]
    public async Task<IResult> Handle(
        SignInCommand req,
        CancellationToken ct
    )
    {
        var user = await _ctx.Users
            .Where(q => q.Email == req.Email)
            .Select(q => new { 
                q.Id,
                q.Password,
                q.Salt 
            })
            .FirstOrDefaultAsync();
        if (user is null)
            throw new NotFoundException(nameof(User));

        if (!SecurityHelper.ValidatePassword(
            req.Password,
            user.Password!,
            user.Salt!
        ))
            throw new InvalidCredentialsException();

        return Results.Ok(new TokensDTO(
            _tokenService.GenerateAccessToken(user.Id),
            _tokenService.GenerateRefreshToken()
        ));
    }

    [HttpPost("auth/sign-up")]
    public async Task<IResult> Handle(
        SignUpCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Users.AnyAsync(q => q.Email == req.Email))
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(User), 
                nameof(User.Email)
            );

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

    [HttpPost("auth/token/refresh")]
    public async Task<IResult> Handle(
        RefreshAccessTokenCommand req,
        CancellationToken ct
    )
    {
        var refreshToken = await _refreshTokensRepo.GetStringAsync(req.CurrentUserId.ToString());
        if (refreshToken is null)
            throw new InvalidRefreshTokenException();

        refreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokensRepo.SetStringAsync(
            req.CurrentUserId.ToString(),
            refreshToken
        );

        return Results.Ok(new TokensDTO(
            _tokenService.GenerateAccessToken(req.CurrentUserId),
            refreshToken
        ));
    }

    [HttpPost("auth/token/revoke/{string:userId}")]
    public async Task<IResult> Handle(
        RevokeRefreshTokenCommand req,
        CancellationToken ct
    )
    {
        var refreshToken = await _refreshTokensRepo.GetStringAsync(req.UserId.ToString());
        if (refreshToken is null)
            throw new InvalidRefreshTokenException();

        return Results.Ok();
    }
}