﻿using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Discounts;

[DynamoDBTable("Discounts")]
public record DeleteDiscountDTO
{
    public string? Name { get; init; }

    public double? Value { get; init; }

    public bool? IsActive { get; init; }
}