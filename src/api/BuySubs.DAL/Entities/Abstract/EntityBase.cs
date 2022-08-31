namespace BuySubs.DAL.Entities.Abstract;

public record EntityBase
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}