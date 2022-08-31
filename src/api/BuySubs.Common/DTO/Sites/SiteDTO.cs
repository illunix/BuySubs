namespace BuySubs.Common.DTO.Sites;

public readonly record struct SiteDTO(
    Guid Id,
    string Name,
    bool IsActive
);