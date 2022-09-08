namespace BuySubs.Common.DTO.Orders;

public readonly record struct OrderDTO(
    Guid Id,
    Guid ServiceId,
    DateTime CreatedAt
);