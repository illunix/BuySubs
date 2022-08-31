using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record User : EntityBase
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Salt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}