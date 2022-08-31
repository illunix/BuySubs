using BuySubs.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuySubs.DAL.Context;

public sealed class InternalDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Site> Sites { get; init; }
    public DbSet<Service> Services { get; init; }

    public InternalDbContext(DbContextOptions<InternalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}