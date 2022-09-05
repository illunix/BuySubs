using BuySubs.API.Attributes;
using BuySubs.BLL.Queries.Services;
using BuySubs.Common.DTO.Services;
using BuySubs.DAL.Context;
using BuySubs.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuySubs.BLL.Mappings;

namespace BuySubs.BLL.QueryHandlers;

internal sealed class ServicesQueryHandlers
    : IHttpRequestHandler<GetServicesQuery>
{
    private readonly InternalDbContext _ctx;
    private readonly ServiceMapper _mapper;

    public ServicesQueryHandlers(
        InternalDbContext ctx,
        ServiceMapper mapper
    )
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    [HttpGet("services")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetServicesQuery req,
        CancellationToken ct
    )
        => Results.Ok(_mapper.AdaptToDto(await _ctx.Services.ToListAsync()));
}
