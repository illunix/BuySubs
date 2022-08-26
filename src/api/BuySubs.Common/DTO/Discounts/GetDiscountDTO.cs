using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Discounts;

[DynamoDBTable("Discount")]
public record GetDiscountDTO
{
    public string? Name { get; init; }

    public double? Value { get; init; }

    public bool? IsActive { get; init; }
}