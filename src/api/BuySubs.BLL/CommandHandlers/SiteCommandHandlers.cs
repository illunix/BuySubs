using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Sites;
using MediatR;
using Microsoft.AspNetCore.Http;
using BuySubs.Common.DTO.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySubs.BLL.Exceptions.Sites;
using Microsoft.AspNetCore.Mvc;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class SiteCommandHandlers :
    IRequestHandler<CreateSiteCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public SiteCommandHandlers(
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
        if ((await _ctx.LoadAsync<SiteWithThisNameAlreadyExistDTO>(req.Name)) is not null)
        {
            throw new SiteWithThisNameAlreadyExistException();
        }

        await _ctx.SaveAsync(new CreateSiteDTO
        {
            Name = req.Name
        });

        return Results.Ok();
    }
}
