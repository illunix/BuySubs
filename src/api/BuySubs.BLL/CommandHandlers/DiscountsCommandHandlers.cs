using BuySubs.BLL.Commands.Discounts;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Mappings;
using BuySubs.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuySubs.DAL.Entities;

namespace BuySubs.BLL.CommandHandlers;

public sealed partial class DiscountsCommandHandlers :
    IHttpRequestHandler<CreateDiscountCommand>,
    IHttpRequestHandler<DeleteDiscountCommand>,
    IHttpRequestHandler<UpdateDiscountCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly DiscountMapper _mapper;

    [HttpPost("discounts")]
    public async Task<IResult> Handle(
        CreateDiscountCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Discounts.AnyAsync(q => q.Name == req.Name))
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Discount),
                nameof(Discount.Name)
            );

        _ctx.Add(_mapper.AdaptToEntity(req));

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpPut("discounts")]
    public async Task<IResult> Handle(
        UpdateDiscountCommand req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.Discounts.FirstOrDefaultAsync(q =>
            q.Name == req.Name &&
            q.Id != req.Id
        );

        if (discount is null)
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Discount),
                nameof(Discount.Name)
            );

        _ctx.Update(_mapper.AdaptToEntity(req));

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpDelete("discounts")]
    public async Task<IResult> Handle(
        DeleteDiscountCommand req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.Discounts.FirstOrDefaultAsync(q => q.Id == req.Id);

        if (discount is null)
            throw new NotFoundException(nameof(Discount));

        _ctx.Remove(discount);

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }
}