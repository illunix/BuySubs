using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Sites;
using MediatR;
using Microsoft.AspNetCore.Http;
using BuySubs.Common.DTO.Sites;
using BuySubs.BLL.Exceptions.Sites;
using Microsoft.AspNetCore.Mvc;
using BuySubs.BLL.Exceptions;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class SitesCommandHandlers :
    IRequestHandler<CreateSiteCommand, IResult>,
    IRequestHandler<ActivateSiteCommand, IResult>,
    IRequestHandler<DeactivateSiteCommand, IResult>,
    IRequestHandler<DeleteSiteCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public SitesCommandHandlers(
        IDynamoDBContext ctx
    )
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
            IsActive =  req.IsActive
        });

        return Results.Ok();
    }

    [HttpPost("sites/activate")]
    public async Task<IResult> Handle (
        ActivateSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<ActivateSiteDTO>(req.Name);

        if (site is null)
        {
            throw new NotFoundException(nameof(ActivateSiteDTO));
        }        

        await _ctx.SaveAsync(new ActivateSiteDTO
        {
            Name = req.Name,
            IsActive = true
        });

        return Results.Ok();
    }

    [HttpPost("sites/deactivate")]
    public async Task<IResult> Handle (
        DeactivateSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<ActivateSiteDTO>(req.Name);

        if (site is null)
        {
            throw new NotFoundException(nameof(ActivateSiteDTO));
        }

        await _ctx.SaveAsync(new ActivateSiteDTO
        {
            Name = req.Name,
            IsActive = false
        });

        return Results.Ok();
    }

    [HttpGet]
    public async Task<IResult> Handle()
        => Results.Ok(await _ctx.ScanAsync<GetAllSitesDTO>(default).GetRemainingAsync());

    [HttpDelete]
    public async Task<IResult> Handle(
        DeleteSiteCommand req,
        CancellationToken ct
    )
    {
        var site = await _ctx.LoadAsync<DeleteSiteDTO>(req.Name);

        if ( site is null)
        {
            throw new NotFoundException(nameof(DeleteSiteCommand));
        }

        await _ctx.DeleteAsync(site);

        return Results.Ok();
    }
}
