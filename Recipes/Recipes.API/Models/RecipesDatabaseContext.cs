using Microsoft.EntityFrameworkCore;
using Recipes.API.Models.Entities;

namespace Recipes.API.Models;
public class RecipesDatabaseContext : DbContext
{
    public RecipesDatabaseContext(DbContextOptions<RecipesDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<RecipeEntity> Recipes { get; set; } = null!;
    public DbSet<IngredientEntity> Ingredients { get; set; } = null!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(e => new { e.IngredientId, e.RecipeId });

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(e => e.Ingredient)
            .WithMany(e => e.Recipes)
            .HasForeignKey(e => e.IngredientId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(e => e.Recipe)
            .WithMany(e => e.Ingredients)
            .HasForeignKey(e => e.RecipeId);
    }
}

