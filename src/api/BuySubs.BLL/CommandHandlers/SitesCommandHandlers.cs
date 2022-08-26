using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Sites;
using MediatR;
using Microsoft.AspNetCore.Http;
using BuySubs.BLL.Exceptions.Sites;
using Microsoft.AspNetCore.Mvc;
using BuySubs.BLL.Exceptions;
using BuySubs.Common.DTO.Sites;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class SitesCommandHandlers :
    IRequestHandler<CreateSiteCommand, IResult>,
    IRequestHandler<ActivateSiteCommand, IResult>,
    IRequestHandler<DeactivateSiteCommand, IResult>,
    IRequestHandler<DeleteSiteCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public SitesCommandHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx; 
    }

    [HttpPost("sites")]
    public async Task<IResult> Handle (
        CreateSiteCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.LoadAsync<SiteWithThisNameAlreadyExistDTO>(req.Name) is not null)
        {
            throw new SiteWithThisNameAlreadyExistException();
        }

        await _ctx.SaveAsync(new CreateSiteDTO
        {
            Name = req.Name,
            IsActive = req.IsActive
        });

        return Results.Ok();
    }

    [HttpPost("sites/activate")]
    public async Task<IResult> Handle(
        ActivateSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<ActivateSiteDTO>(req.Name);

        if (site is null)
        {
            throw new NotFoundException(nameof(ActivateSiteDTO));
        }

        if (site.IsActive is true)
        {
            throw new SiteIsAlreadyActiveException();
        }

        await _ctx.SaveAsync(new ActivateSiteDTO
        {
            Name = req.Name,
            IsActive = true
        });

        return Results.Ok();
    }

    [HttpPost("sites/deactivate")]
    public async Task<IResult> Handle(
        DeactivateSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<DeactivateSiteDTO>(req.Name);

        if (site is null)
        {
            throw new NotFoundException(nameof(DeactivateSiteDTO));
        }

        if (site.IsActive is false)
        {
            throw new SiteIsAlreadyInactiveException();
        }

        await _ctx.SaveAsync(new DeactivateSiteDTO
        {
            Name = req.Name,
            IsActive = false
        });

        return Results.Ok();
    }

    [HttpDelete("sites")]
    public async Task<IResult> Handle(
        DeleteSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<DeleteSiteDTO>(req.Name);

        if (site is null)
        {
            throw new NotFoundException(nameof(DeleteSiteDTO));
        }

        await _ctx.DeleteAsync(site);

        return Results.Ok();
    }
}
