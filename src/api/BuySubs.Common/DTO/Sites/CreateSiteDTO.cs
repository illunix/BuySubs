using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Sites;

[DynamoDBTable("Sites")]
public record CreateSiteDTO
{
    public required string? Name { get; init; }

    public required bool? IsActive { get; init; }
}