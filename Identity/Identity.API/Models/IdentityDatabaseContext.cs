using Identity.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Models;
public class IdentityDatabaseContext : DbContext
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(e => new { e.Username }).IsUnique();
    }
}

