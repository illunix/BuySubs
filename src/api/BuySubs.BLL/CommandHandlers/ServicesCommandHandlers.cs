using BuySubs.BLL.Commands.Services;
using BuySubs.BLL.Exceptions;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class ServicesCommandHandlers :
    IRequestHandler<CreateServiceCommand, IResult>,
    IRequestHandler<UpdateServiceCommand, IResult>
{
    private readonly InternalDbContext _ctx;

    public ServicesCommandHandlers(InternalDbContext ctx)
        => _ctx = ctx;

    [HttpPost("services")]
    public async Task<IResult> Handle(
        CreateServiceCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Services.AnyAsync(q => q.Name == req.Name))
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Service),
                nameof(Service.Name)
            );

        _ctx.Add(new Service
        {
            Name = req.Name,
            Price = req.Price,
            Description = req.Description,
        });

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpPut("services")]
    public async Task<IResult> Handle(
        UpdateServiceCommand req,
        CancellationToken ct
    )
    {
        var service = await _ctx.Services.FirstOrDefaultAsync(q =>
            q.Name == req.Name &&
            q.Id != req.Id
        );
        if (service is null)
            throw new EntityWithSamePropertyValueAlreadyExistException(
                nameof(Service),
                nameof(Service.Name)
            );

        _ctx.Update(service with
        {
            Name = req.Name,
            Description = req.Description,
            Price = req.Price,
            IsActive = req.IsActive
        });

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpDelete("services")]
    public async Task<IResult> Handle(
        DeleteServiceCommand req,
        CancellationToken ct
    )
    {
        var service = await _ctx.Services.FirstOrDefaultAsync(q => q.Id == req.Id);
        if (service is null)
            throw new NotFoundException(nameof(Service));

        _ctx.Remove(service);

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }
}