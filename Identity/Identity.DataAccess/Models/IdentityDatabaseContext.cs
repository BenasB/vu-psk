using Identity.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess;
public class IdentityDatabaseContext : DbContext
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options) { }

    public DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(e => new { e.Username }).IsUnique();
    }
}

