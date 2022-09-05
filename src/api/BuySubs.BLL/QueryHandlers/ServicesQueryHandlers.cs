using BuySubs.BLL.Queries.Services;
using BuySubs.Common.DTO.Services;
using BuySubs.DAL.Context;
using BuySubs.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuySubs.BLL.Mappings;

namespace BuySubs.BLL.QueryHandlers;

public sealed partial class ServicesQueryHandlers
    : IHttpRequestHandler<GetServicesQuery>
{
    private readonly InternalDbContext _ctx;
    private readonly ServiceMapper _mapper;

    [HttpGet("services")]
    public async Task<IResult> Handle(
        GetServicesQuery req,
        CancellationToken ct
    )
        => Results.Ok(_mapper.AdaptToDto(await _ctx.Services.ToListAsync()));
}
