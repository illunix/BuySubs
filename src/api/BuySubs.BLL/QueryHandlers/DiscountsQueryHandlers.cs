using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Queries.Discounts;
using BuySubs.Common.DTO.Discounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;
using BuySubs.BLL.Exceptions;

namespace BuySubs.BLL.QueryHandlers;

internal class DiscountsQueryHandlers :
    IRequestHandler<GetDiscountsQuery, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public DiscountsQueryHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet("discounts")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetDiscountsQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.ScanAsync<GetDiscountsDTO>(default).GetRemainingAsync());

    [HttpGet("discount")]
    public async Task<IResult> Handle(
        GetDiscountQuery req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.LoadAsync<GetDiscountDTO>(req.Name);

        if (discount is null)
        {
            throw new NotFoundException(nameof(GetDiscountDTO));
        }

        return Results.Ok(discount);
    }
       
}