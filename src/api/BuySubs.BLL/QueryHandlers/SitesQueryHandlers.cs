using BuySubs.BLL.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;
using BuySubs.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BuySubs.Common.DTO.Sites;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Queries.Sites;

namespace BuySubs.BLL.QueryHandlers;

internal class SitesQueryHandlers :
    IHttpRequestHandler<GetSitesQuery>
{
    private readonly InternalDbContext _ctx;

    public SitesQueryHandlers(InternalDbContext ctx)
        => _ctx = ctx;

    [HttpGet("sites")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetSitesQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.Sites.Select(q => new SiteDTO(
            q.Id,
            q.Name,
            q.IsActive
        )).ToListAsync());
}