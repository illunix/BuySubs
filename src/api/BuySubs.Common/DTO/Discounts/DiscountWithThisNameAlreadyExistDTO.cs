using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Discounts;

[DynamoDBTable("Discounts")]
public record DiscountWithThisNameAlreadyExistDTO
{
    public string? Name { get; init; }
}