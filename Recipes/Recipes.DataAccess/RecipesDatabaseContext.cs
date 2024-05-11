using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess;
public class RecipesDatabaseContext : DbContext
{
    public RecipesDatabaseContext(DbContextOptions<RecipesDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<RecipeEntity> Recipes { get; set; } = null!;
}

