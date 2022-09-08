using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Mappings;
using BuySubs.BLL.Queries.Orders;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.QueryHandlers;

public sealed partial class OrdersQueryHandlers : 
    IHttpRequestHandler<GetOrdersQuery>,
    IHttpRequestHandler<GetOrderQuery>
{
    private readonly InternalDbContext _ctx;
    private readonly OrderMapper _mapper;

    [HttpGet("orders")]
    public async Task<IResult> Handle(
        GetOrdersQuery req,
        CancellationToken ct
    )
        => Results.Ok(_mapper.AdaptToDto(await _ctx.Orders
            .AsNoTracking()
            .ToListAsync()
        ));

    [HttpGet("order")]
    public async Task<IResult> Handle(
        GetOrderQuery req,
        CancellationToken ct
    )
    {
        var order = await _ctx.Orders.FirstOrDefaultAsync(q => q.Id == req.Id);
        if (order is null)
            throw new NotFoundException(nameof(Order));

        return Results.Ok(order);
    }
}