namespace BuySubs.Common.DTO.Discounts;

public readonly record struct DiscountDTO(
    Guid Id,
    string Name,
    double Value,
    bool IsActive
);

