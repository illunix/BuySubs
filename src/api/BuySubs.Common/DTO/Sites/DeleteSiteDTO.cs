using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Sites;

[DynamoDBTable("Sites")]
public record DeleteSiteDTO
{
    public string? Name { get; init; }
}
