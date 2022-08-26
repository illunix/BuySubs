using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Discounts;

[DynamoDBTable("Discounts")]
public record CreateDiscountDTO
{
    public required string? Name { get; init; }

    public required double? Value { get; init; }

    public required bool? IsActive { get; init; }
}