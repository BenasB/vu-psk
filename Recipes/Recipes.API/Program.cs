using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess;
using Recipes.DataAccess.Entities;
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

app.MapGet("/recipes", async (RecipesDatabaseContext dbContext) =>
{
    var recipe = new RecipeEntity()
    {
        Title = "Mom's Spaghetti",
        Description = "BlaBla",
        AuthorId = 1,
        CookingTime = new TimeSpan(0, 2, 0, 0),
        UpdatedAt = DateTime.UtcNow,
        Servings = 3,
        Instructions = new List<string>
        {
            "Cook spaghetti noodles according to package instructions.",
            "In a large skillet, brown ground beef with chopped onion and minced garlic.",
            "Add tomato sauce, Italian seasoning, salt, and pepper to the skillet. Simmer for 10 minutes.",
            "Serve the spaghetti topped with the meat sauce and sprinkle with Parmesan cheese."
        },
        Ingredients = new List<string>
        {
            "Spaghetti noodles",
            "Ground beef"
        }
    };

    dbContext.Recipes.Add(recipe);
    await dbContext.SaveChangesAsync();

    return dbContext.Recipes
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
        .ToList();
});

app.Run();