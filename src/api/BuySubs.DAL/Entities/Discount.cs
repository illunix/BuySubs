using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record Discount : EntityBase
{
    public string Name { get; set; }

    public double Value { get; set; }

    public bool IsActive { get; set; }
}
