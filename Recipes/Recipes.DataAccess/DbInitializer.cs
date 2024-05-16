using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess;

public static class DbInitializer
{
    private static RecipeEntity[] SeedRecipesData => CreateSeedRecipesData();

    public static async Task SeedRecipes(RecipesDatabaseContext context)
    {
        await context.Recipes.AddRangeAsync(SeedRecipesData);
        await context.SaveChangesAsync();
    }
    
    private static RecipeEntity[] CreateSeedRecipesData()
    {
        return
        [
            new RecipeEntity
            {
                Title = "Classic Marinara Pasta",
                Description = "A simple yet delicious pasta dish with homemade marinara sauce.",
                AuthorId = 2,
                CookingTime = new TimeSpan(0, 1, 0, 0),
                UpdatedAt = DateTime.UtcNow,
                Servings = 4,
                Instructions = new List<string>
                {
                    "Cook spaghetti noodles according to package instructions.",
                    "In a saucepan, heat olive oil and sauté minced garlic until fragrant.",
                    "Add crushed tomatoes, basil, oregano, salt, and pepper. Simmer for 20 minutes.",
                    "Toss cooked spaghetti in the marinara sauce until fully coated.",
                    "Garnish with fresh basil leaves and grated Parmesan cheese before serving."
                },
                Ingredients = new List<string>
                {
                    "Spaghetti noodles",
                    "Garlic",
                    "Crushed tomatoes",
                    "Fresh basil",
                    "Olive oil"
                }
            },
            new RecipeEntity
            {
                Title = "Creamy Mushroom Alfredo",
                Description = "Indulge in the rich and creamy flavors of this mushroom Alfredo pasta.",
                AuthorId = 3,
                CookingTime = new TimeSpan(0, 0, 45, 0),
                UpdatedAt = DateTime.UtcNow,
                Servings = 3,
                Instructions = new List<string>
                {
                    "Cook fettuccine noodles according to package instructions.",
                    "In a skillet, sauté sliced mushrooms in butter until golden brown.",
                    "Add heavy cream, minced garlic, grated Parmesan cheese, salt, and pepper. Simmer for 10 minutes.",
                    "Toss cooked fettuccine in the creamy mushroom sauce until well combined.",
                    "Garnish with chopped parsley and additional Parmesan cheese before serving."
                },
                Ingredients = new List<string>
                {
                    "Fettuccine noodles",
                    "Mushrooms",
                    "Garlic",
                    "Heavy cream",
                    "Butter"
                }
            },
            new RecipeEntity
            {
                Title = "Spicy Arrabbiata Penne",
                Description = "A fiery and flavorful pasta dish with a kick of spice.",
                AuthorId = 4,
                CookingTime = new TimeSpan(0, 0, 40, 0),
                UpdatedAt = DateTime.UtcNow,
                Servings = 4,
                Instructions = new List<string>
                {
                    "Cook penne pasta according to package instructions.",
                    "In a pan, heat olive oil and sauté minced garlic until lightly browned.",
                    "Add chopped tomatoes, red chili flakes, basil, salt, and pepper. Simmer for 15 minutes.",
                    "Toss cooked penne in the spicy arrabbiata sauce until fully coated.",
                    "Sprinkle with grated Pecorino Romano cheese and chopped fresh parsley before serving."
                },
                Ingredients = new List<string>
                {
                    "Penne pasta",
                    "Garlic",
                    "Tomatoes",
                    "Red chili flakes",
                    "Fresh basil"
                }
            },
            new RecipeEntity
            {
                Title = "Lemon Garlic Shrimp Linguine",
                Description = "Brighten up your day with this zesty and savory shrimp linguine.",
                AuthorId = 5,
                CookingTime = new TimeSpan(0, 0, 30, 0),
                UpdatedAt = DateTime.UtcNow,
                Servings = 2,
                Instructions = new List<string>
                {
                    "Cook linguine pasta according to package instructions.",
                    "In a skillet, sauté shrimp in olive oil and minced garlic until pink and cooked through.",
                    "Add lemon zest, lemon juice, chopped parsley, salt, and pepper to the skillet. Cook for 5 minutes.",
                    "Toss cooked linguine with the lemon garlic shrimp mixture until well combined.",
                    "Serve immediately with a sprinkle of grated Parmesan cheese."
                },
                Ingredients = new List<string>
                {
                    "Linguine pasta",
                    "Shrimp",
                    "Garlic",
                    "Lemon",
                    "Fresh parsley"
                }
            },
            new RecipeEntity
            {
                Title = "Pesto Tortellini with Sun-Dried Tomatoes",
                Description = "Experience the vibrant flavors of basil pesto and sun-dried tomatoes in this tortellini dish.",
                AuthorId = 6,
                CookingTime = new TimeSpan(0, 0, 25, 0),
                UpdatedAt = DateTime.UtcNow,
                Servings = 4,
                Instructions = new List<string>
                {
                    "Cook tortellini pasta according to package instructions.",
                    "In a bowl, toss cooked tortellini with basil pesto until evenly coated.",
                    "Add chopped sun-dried tomatoes, pine nuts, and a drizzle of olive oil. Mix well.",
                    "Garnish with fresh basil leaves and grated Parmesan cheese before serving."
                },
                Ingredients = new List<string>
                {
                    "Tortellini pasta",
                    "Basil pesto",
                    "Sun-dried tomatoes",
                    "Pine nuts",
                    "Olive oil"
                }
            },
        ];
    }
}