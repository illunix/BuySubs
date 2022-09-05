using BuySubs.BLL.Commands.Sites;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Sites;
using BuySubs.BLL.Interfaces;
using BuySubs.BLL.Mappings;
using BuySubs.DAL.Context;
using BuySubs.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.BLL.CommandHandlers;

public sealed partial class SitesCommandHandlers :
    IHttpRequestHandler<CreateSiteCommand>,
    IHttpRequestHandler<UpdateSiteCommand>,
    IHttpRequestHandler<DeleteSiteCommand>
{
    private readonly InternalDbContext _ctx;
    private readonly SiteMapper _mapper;

    [HttpPost("sites")]
    public async Task<IResult> Handle(
        CreateSiteCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.Sites.AnyAsync(q => q.Name == req.Name))
            throw new SiteWithThisNameAlreadyExistException();

        _ctx.Add(_mapper.AdaptToEntity(req));

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

        _ctx.Update(_mapper.AdaptToEntity(req));

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