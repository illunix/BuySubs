using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Mappings;
using BuySubs.BLL.Queries.Discounts;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.QueryHandlers;

public sealed partial class DiscountsQueryHandlers :
    IHttpRequestHandler<GetDiscountsQuery>,
    IHttpRequestHandler<GetDiscountQuery>
{
    private readonly InternalDbContext _ctx;
    private readonly DiscountMapper _mapper;

    [HttpGet("discounts")]
    public async Task<IResult> Handle(
        GetDiscountsQuery req,
        CancellationToken ct
    )
        => Results.Ok(_mapper.AdaptToDto(await _ctx.Discounts
            .AsNoTracking()
            .ToListAsync()
        ));

    [HttpGet("discount")]
    public async Task<IResult> Handle(
        GetDiscountQuery req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.Discounts
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == req.Id);
        if (discount is null)
            throw new NotFoundException(nameof(Discount));

        return Results.Ok(discount);
    }
}