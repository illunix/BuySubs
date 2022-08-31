using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record Site : EntityBase
{
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}