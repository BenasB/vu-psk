using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;

namespace Recipes.DataAccess;

public class RecipesDatabaseContext(DbContextOptions<RecipesDatabaseContext> options) : DbContext(options)
{
    public DbSet<RecipeEntity> Recipes { get; init; } = null!;
    public DbSet<TagEntity> Tags { get; init; } = null!;
    public DbSet<TagRecipeEntity> TagRecipes { get; set; } = null!;

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
}