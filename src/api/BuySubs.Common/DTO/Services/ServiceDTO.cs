namespace BuySubs.Common.DTO.Services;

public readonly record struct ServiceDTO(
    Guid Id,
    string Name,
    bool IsActive
);