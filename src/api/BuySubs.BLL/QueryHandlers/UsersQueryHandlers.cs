using BuySubs.BLL.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;
using BuySubs.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.QueryHandlers;

internal class UsersQueryHandlers :
    IHttpRequestHandler<GetUsersCountQuery>
{
    private readonly InternalDbContext _ctx;

    public UsersQueryHandlers(InternalDbContext ctx)
        => _ctx = ctx;

    [HttpGet("users")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetUsersCountQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.Users.CountAsync());
}