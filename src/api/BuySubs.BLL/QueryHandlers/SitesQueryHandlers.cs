using MediatR;
using Microsoft.AspNetCore.Http;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;
using BuySubs.BLL.Queries.Sites;
using BuySubs.Common.DTO.Sites;

namespace BuySubs.BLL.QueryHandlers;

internal class SitesQueryHandlers :
    IRequestHandler<GetSitesQuery, IResult>
{
    private readonly IDynamoDBContext _ctx;
    
    public SitesQueryHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet("sites")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetSitesQuery req,
        CancellationToken ct
    )
        => Results.Ok(await _ctx.ScanAsync<GetSitesDTO>(default).GetRemainingAsync());
}