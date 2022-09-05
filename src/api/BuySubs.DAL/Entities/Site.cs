using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record Site : EntityBase
{
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Service>? Services { get; init; }
}