using BuySubs.BLL.Commands.Orders;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Mappings;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.CommandHandlers;

public sealed partial class OrdersCommandHandlers :
    IHttpRequestHandler<CreateOrderCommand>,
    IHttpRequestHandler<UpdateOrderCommand>,
    IHttpRequestHandler<DeleteOrderCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly OrderMapper _mapper;

    [HttpPost("orders")]
    public async Task<IResult> Handle(
        CreateOrderCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Orders.AnyAsync(q => q.ServiceId == req.ServiceId))
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Order),
                nameof(Order.ServiceId)
            );

        _ctx.Add(_mapper.AdaptToEntity(req));


        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpPut("orders")]
    public async Task<IResult> Handle(
        UpdateOrderCommand req,
        CancellationToken ct
    )
    {
        var order = await _ctx.Orders.FirstOrDefaultAsync(q =>
            q.ServiceId == req.ServiceId &&
            q.Id != req.Id
        );
        if (order is null)
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Order),
                nameof(Order.ServiceId)
            );

        _ctx.Update(_mapper.AdaptToEntity(req));
        
        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpDelete("orders")]
    public async Task<IResult> Handle(
        DeleteOrderCommand req,
        CancellationToken ct
    )
    {
        var order = await _ctx.Orders.FirstOrDefaultAsync(q => q.Id == req.Id);
        if (order is null)
            throw new NotFoundException(nameof(Order));

        _ctx.Remove(order);

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }
}