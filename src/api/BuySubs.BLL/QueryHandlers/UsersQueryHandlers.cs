using BuySubs.BLL.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;
using BuySubs.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.QueryHandlers;

public sealed partial class UsersQueryHandlers :
    IHttpRequestHandler<GetUsersCountQuery>
{
    private readonly InternalDbContext _ctx;

    [HttpGet("users/count")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetUsersCountQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.Users.CountAsync());
}