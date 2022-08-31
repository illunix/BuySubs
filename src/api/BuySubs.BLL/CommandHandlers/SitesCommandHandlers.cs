using BuySubs.BLL.Commands.Sites;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Sites;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class SitesCommandHandlers :
    IRequestHandler<CreateSiteCommand, IResult>,
    IRequestHandler<UpdateSiteCommand, IResult>,
    IRequestHandler<DeleteSiteCommand, IResult>
{
    private readonly InternalDbContext _ctx;

    public SitesCommandHandlers(InternalDbContext ctx)
        => _ctx = ctx;

    [HttpPost("sites")]
    public async Task<IResult> Handle(
        CreateSiteCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Sites.AnyAsync(q => q.Name == req.Name))
            throw new SiteWithThisNameAlreadyExistException();

        _ctx.Add(new Site {
            Name = req.Name, 
            IsActive = true 
        });

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpPut("sites")]
    public async Task<IResult> Handle(
        UpdateSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.Sites.FirstOrDefaultAsync(q => q.Id == req.Id);
        if (site is null)
            throw new NotFoundException(nameof(Site));

        _ctx.Update(site with
        {
            Name = req.Name,
            IsActive = req.IsActive
        });

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpDelete("sites")]
    public async Task<IResult> Handle(
        DeleteSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.Sites.FirstOrDefaultAsync(q => q.Id == req.Id);
        if (site is null)
            throw new NotFoundException(nameof(Site));

        _ctx.Remove(site);

        await _ctx.SaveChangesAsync();

        return Results.Ok();
    }
}