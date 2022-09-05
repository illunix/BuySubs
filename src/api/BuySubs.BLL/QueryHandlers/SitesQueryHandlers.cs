using BuySubs.BLL.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuySubs.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BuySubs.Common.DTO.Sites;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Queries.Sites;
using BuySubs.BLL.Mappings;

namespace BuySubs.BLL.QueryHandlers;

public sealed partial class SitesQueryHandlers :
    IHttpRequestHandler<GetSitesQuery>
{
    private readonly InternalDbContext _ctx;
    private readonly SiteMapper _mapper;

    [HttpGet("sites")]
    public async Task<IResult> Handle(
        GetSitesQuery req,
        CancellationToken ct
    )
        => Results.Ok(_mapper.AdaptToDto(await _ctx.Sites.ToListAsync()));
}