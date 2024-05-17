using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess;
using Recipes.DataAccess.Interfaces;
using Recipes.DataAccess.Repositories;
using Recipes.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipesDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var recipesDbContext = scope.ServiceProvider.GetRequiredService<RecipesDatabaseContext>();
    recipesDbContext.Database.Migrate();

    if (app.Environment.IsDevelopment() && !recipesDbContext.Recipes.Any())
        await DbInitializer.SeedRecipes(recipesDbContext);
}

app.MapGet("/recipes", async (RecipesDatabaseContext dbContext) =>
{
    return await dbContext.Recipes
        .Select(r => new Recipe
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            CookingTime = r.CookingTime,
            Servings = r.Servings,
            AuthorId = r.AuthorId,
            UpdatedAt = r.UpdatedAt,
            Ingredients = r.Ingredients,
            Instructions = r.Instructions
        })
        .ToListAsync();
});

app.Run();