using BuySubs.BLL.Commands.Services;
using BuySubs.BLL.Exceptions;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using BuySubs.BLL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuySubs.BLL.Mappings;

namespace BuySubs.BLL.CommandHandlers;

public sealed partial class ServicesCommandHandlers :
    IHttpRequestHandler<CreateServiceCommand>,
    IHttpRequestHandler<UpdateServiceCommand>,
    IHttpRequestHandler<DeleteServiceCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly ServiceMapper _mapper;

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

        _ctx.Add(_mapper.AdaptToEntity(req));

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

        _ctx.Update(_mapper.AdaptToEntity(req));

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
