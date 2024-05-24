using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Recipes.DataAccess;

public class RecipesDatabaseContext(DbContextOptions<RecipesDatabaseContext> options) : DbContext(options)
{
    public DbSet<RecipeEntity> Recipes { get; init; } = null!;
    public DbSet<TagEntity> Tags { get; init; } = null!;
    public DbSet<TagRecipeEntity> TagRecipes { get; set; } = null!;

    public override int SaveChanges()
    {
        HandleConflicts();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleConflicts();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>()
            .HasIndex(tag => tag.Name)
            .IsUnique();

        modelBuilder.Entity<TagRecipeEntity>()
            .HasKey(rt => new { rt.RecipeId, rt.TagId });

        modelBuilder.Entity<TagRecipeEntity>()
            .HasOne(rt => rt.Recipe)
            .WithMany(r => r.Tags)
            .HasForeignKey(rt => rt.RecipeId);

        modelBuilder.Entity<TagRecipeEntity>()
            .HasOne(rt => rt.Tag)
            .WithMany(t => t.Recipes)
            .HasForeignKey(rt => rt.TagId);
    }

    private void HandleConflicts()
    {
        var modifiedEntries = ChangeTracker.Entries().Where(e => e.Entity is RecipeEntity && e.State == EntityState.Modified).ToList();
        var addedEntries = ChangeTracker.Entries().Where(e => e.Entity is RecipeEntity && e.State == EntityState.Added).ToList();

        CheckForConflict(modifiedEntries);
        UpdateRowVersions(modifiedEntries.Union(addedEntries));
    }

    private void CheckForConflict(IEnumerable<EntityEntry> entriesToCheck)
    {
        foreach (var entry in entriesToCheck)
        {
            var entity = (RecipeEntity)entry.Entity;
            var originalRowVersion = entry.OriginalValues[nameof(RecipeEntity.RowVersion)] as Guid?;

            if (originalRowVersion != entity.RowVersion)
                throw new DbUpdateConcurrencyException();
        }
    }

    private void UpdateRowVersions(IEnumerable<EntityEntry> entriesToUpdate)
    {
        foreach (var entry in entriesToUpdate)
        {
            var entity = (RecipeEntity)entry.Entity;
            entity.RowVersion = Guid.NewGuid();
        }
    }
}