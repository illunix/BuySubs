using BuySubs.API.Attributes;
using BuySubs.BLL.Queries.Services;
using BuySubs.Common.DTO.Services;
using BuySubs.DAL.Context;
using BuySubs.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.QueryHandlers;

internal sealed class ServicesQueryHandlers
    : IHttpRequestHandler<GetServicesQuery>
{
    private readonly InternalDbContext _ctx;

    public ServicesQueryHandlers(InternalDbContext ctx)
        => _ctx = ctx;

    [HttpGet("services")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetServicesQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.Services.Select(q => new ServiceDTO(
            q.Id,
            q.Name,
            q.IsActive
        )).ToListAsync());
}
