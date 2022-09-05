using BuySubs.DAL.Entities.Abstract;

namespace BuySubs.DAL.Entities;

public sealed record Service : EntityBase
{
    public Guid SiteId { get; init; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}