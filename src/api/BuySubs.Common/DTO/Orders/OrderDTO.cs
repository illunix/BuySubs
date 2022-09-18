namespace BuySubs.Common.DTO.Orders;

public readonly record struct OrderDTO(
    Guid Id,
    Guid UserId,
    Guid ServiceId,
    DateTime CreatedAt
);