using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess;

public class RecipesDatabaseContext(DbContextOptions<RecipesDatabaseContext> options) : DbContext(options)
{
    public DbSet<RecipeEntity> Recipes { get; init; } = null!;
}