using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using BuySubs.BLL.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using BuySubs.API.Attributes;

namespace BuySubs.BLL.QueryHandlers;

internal class UsersCommandHandlers :
    IRequestHandler<GetUsersCountQuery, IResult>
{
    private readonly IAmazonDynamoDB _ctx;

    public UsersCommandHandlers(IAmazonDynamoDB ctx)
    {
        _ctx = ctx;
    }

    [HttpGet("users")]
    [NoValidation]
    public async Task<IResult> Handle(
        GetUsersCountQuery req,
        CancellationToken ct
    )
        => Results.Ok((await _ctx.ScanAsync(new() { TableName = "Users", Select = Select.COUNT })).Count);
}