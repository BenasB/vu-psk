using Recipes.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/recipes", () =>
    {
        var recipes = new Recipe[]
        {
            new()
            {
                Id = Guid.Empty,
                Title = "Mom's Spaghetti",
                Ingredients = new List<Ingredient>
                {
                    new() { Name = "Spaghetti noodles", Quantity = "8 ounces" },
                    new() { Name = "Ground beef", Quantity = "1 pound" },
                    new() { Name = "Tomato sauce", Quantity = "24 ounces" },
                    new() { Name = "Onion", Quantity = "1, chopped" },
                    new() { Name = "Garlic cloves", Quantity = "2, minced" },
                    new() { Name = "Italian seasoning", Quantity = "1 teaspoon" },
                    new() { Name = "Salt", Quantity = "to taste" },
                    new() { Name = "Pepper", Quantity = "to taste" },
                    new() { Name = "Parmesan cheese", Quantity = "for topping" }
                },
                Instructions = new List<string>
                {
                    "Cook spaghetti noodles according to package instructions.",
                    "In a large skillet, brown ground beef with chopped onion and minced garlic.",
                    "Add tomato sauce, Italian seasoning, salt, and pepper to the skillet. Simmer for 10 minutes.",
                    "Serve the spaghetti topped with the meat sauce and sprinkle with Parmesan cheese."
                }
            }
        };
        
        return recipes;
    });

app.Run();