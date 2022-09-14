using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record Order : EntityBase
{
    public required Guid UserId { get; init; }

    public required Guid ServiceId { get; init; }

    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}