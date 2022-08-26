using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Sites;

[DynamoDBTable("Sites")]
public record DeactivateSiteDTO
{
    public string? Name { get; init; }

    public bool? IsActive { get; init; }
}
