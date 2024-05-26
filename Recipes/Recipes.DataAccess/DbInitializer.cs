using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;

namespace Recipes.DataAccess;

public static class DbInitializer
{
    private static RecipeEntity[] SeedRecipesData => CreateSeedRecipesData();
    private static TagEntity[] SeedTagsData => CreateSeedTagsData();

    public static async Task SeedDatabase(RecipesDatabaseContext context)
    {
        await context.Tags.AddRangeAsync(SeedTagsData);
        await context.Recipes.AddRangeAsync(SeedRecipesData);
        await context.SaveChangesAsync();
    }

    private static RecipeEntity[] CreateSeedRecipesData()
    {
        return
        [
            new RecipeEntity
            {
                Title = "Grilled Lemon Herb Chicken",
                Description = "Juicy and flavorful chicken breasts marinated in a lemon herb mixture and grilled to perfection.",
                CookingTime = new TimeSpan(0, 0, 30, 0),
                Servings = 4,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a bowl, mix lemon juice, olive oil, garlic, herbs, salt, and pepper.",
                    "Marinate chicken breasts in the mixture for at least 15 minutes.",
                    "Preheat grill to medium-high heat.",
                    "Grill chicken breasts for 6-7 minutes per side or until fully cooked.",
                    "Serve with a side of vegetables or salad."
                },
                Ingredients = new List<string>
                {
                    "Chicken breasts",
                    "Lemon juice",
                    "Olive oil",
                    "Garlic",
                    "Fresh herbs (rosemary, thyme)",
                    "Salt",
                    "Pepper"
                },
                Image = "https://simply-delicious-food.com/wp-content/uploads/2019/05/lemon-herb-grilled-chicken-breast-3.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 6 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },new RecipeEntity
            {
                Title = "Vegetarian Stuffed Peppers",
                Description = "Colorful bell peppers stuffed with a hearty mixture of rice, beans, and vegetables.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 4,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat oven to 375°F (190°C).",
                    "Cut tops off bell peppers and remove seeds.",
                    "In a bowl, mix cooked rice, black beans, corn, tomatoes, and spices.",
                    "Stuff the bell peppers with the rice mixture.",
                    "Place stuffed peppers in a baking dish and bake for 30-35 minutes.",
                    "Serve hot, garnished with fresh cilantro."
                },
                Ingredients = new List<string>
                {
                    "Bell peppers",
                    "Cooked rice",
                    "Black beans",
                    "Corn",
                    "Diced tomatoes",
                    "Spices (cumin, paprika)",
                    "Fresh cilantro"
                },
                Image = "https://cdn.loveandlemons.com/wp-content/uploads/2023/08/vegetarian-stuffed-peppers.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
                        new RecipeEntity
            {
                Title = "Vegan Chocolate Chip Cookies",
                Description = "Soft and chewy chocolate chip cookies made without any animal products, perfect for a vegan treat.",
                CookingTime = new TimeSpan(0, 0, 25, 0),
                Servings = 12,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "In a bowl, whisk together flour, baking soda, and salt.",
                    "In another bowl, beat together coconut oil, brown sugar, and vanilla extract until creamy.",
                    "Add almond milk and mix well.",
                    "Gradually add the dry ingredients to the wet ingredients and mix until combined.",
                    "Fold in the chocolate chips.",
                    "Drop spoonfuls of dough onto a baking sheet.",
                    "Bake for 10-12 minutes, or until golden brown around the edges.",
                    "Let cool on the baking sheet for a few minutes before transferring to a wire rack to cool completely."
                },
                Ingredients = new List<string>
                {
                    "Flour",
                    "Baking soda",
                    "Salt",
                    "Coconut oil",
                    "Brown sugar",
                    "Vanilla extract",
                    "Almond milk",
                    "Chocolate chips"
                },
                Image = "https://bakerbynature.com/wp-content/uploads/2016/01/veganchocolatechipcookies1-1-of-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 6 }
                ]
            },
            new RecipeEntity
            {
                Title = "Vegetable Stir-Fry",
                Description = "A quick and easy vegetable stir-fry made with fresh veggies and a savory sauce, perfect for a budget-friendly meal.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Heat oil in a large skillet or wok over medium-high heat.",
                    "Add minced garlic and sauté until fragrant.",
                    "Add sliced bell peppers, carrots, broccoli, and snap peas. Stir-fry for 5-7 minutes until tender-crisp.",
                    "In a small bowl, whisk together soy sauce, cornstarch, and water. Pour over the vegetables.",
                    "Cook for another 2-3 minutes until the sauce thickens and coats the vegetables.",
                    "Serve hot over cooked rice or noodles."
                },
                Ingredients = new List<string>
                {
                    "Oil",
                    "Garlic",
                    "Bell peppers",
                    "Carrots",
                    "Broccoli",
                    "Snap peas",
                    "Soy sauce",
                    "Cornstarch",
                    "Water",
                    "Rice or noodles"
                },
                Image = "https://natashaskitchen.com/wp-content/uploads/2020/08/Vegetable-Stir-Fry-2.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 10 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 1 }
                ]
            },
            new RecipeEntity
            {
                Title = "Lentil Soup",
                Description = "A hearty and nutritious lentil soup made with simple ingredients, perfect for a budget-friendly and filling meal.",
                CookingTime = new TimeSpan(0, 0, 45, 0),
                Servings = 6,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Heat oil in a large pot over medium heat.",
                    "Add chopped onions, carrots, and celery. Sauté until softened.",
                    "Add minced garlic and cook for another minute.",
                    "Stir in dried lentils, diced tomatoes, vegetable broth, and spices.",
                    "Bring to a boil, then reduce heat and simmer for 30-35 minutes until lentils are tender.",
                    "Season with salt and pepper to taste.",
                    "Serve hot, garnished with fresh parsley."
                },
                Ingredients = new List<string>
                {
                    "Oil",
                    "Onions",
                    "Carrots",
                    "Celery",
                    "Garlic",
                    "Dried lentils",
                    "Diced tomatoes",
                    "Vegetable broth",
                    "Spices (cumin, paprika, bay leaf)",
                    "Salt",
                    "Pepper",
                    "Fresh parsley"
                },
                Image = null,
                Tags =
                [
                    new TagRecipeEntity { TagId = 10 },
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Black Bean Tacos",
                Description = "Flavorful black bean tacos made with simple ingredients, perfect for a quick, easy, and budget-friendly meal.",
                CookingTime = new TimeSpan(0, 0, 15, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Heat oil in a skillet over medium heat.",
                    "Add chopped onions and sauté until softened.",
                    "Stir in black beans, cumin, chili powder, and a splash of water.",
                    "Cook for 5-7 minutes until beans are heated through and flavors are combined.",
                    "Warm the tortillas in a separate pan or microwave.",
                    "Spoon the black bean mixture onto each tortilla.",
                    "Top with diced tomatoes, shredded lettuce, and any other desired toppings.",
                    "Serve immediately."
                },
                Ingredients = new List<string>
                {
                    "Oil",
                    "Onions",
                    "Canned black beans",
                    "Cumin",
                    "Chili powder",
                    "Water",
                    "Tortillas",
                    "Tomatoes",
                    "Lettuce"
                },
                Image = "https://www.cookingclassy.com/wp-content/uploads/2017/02/black-bean-tacos-11.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 10 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 1 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Peanut Butter Cookies",
                Description = "Classic and chewy peanut butter cookies, made with simple ingredients and perfect for a sweet treat.",
                CookingTime = new TimeSpan(0, 0, 25, 0),
                Servings = 24,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "In a large bowl, cream together peanut butter, sugar, and brown sugar until smooth.",
                    "Beat in the egg and vanilla extract.",
                    "In another bowl, whisk together flour, baking soda, and salt.",
                    "Gradually add the dry ingredients to the peanut butter mixture and mix until well combined.",
                    "Roll the dough into small balls and place them on a baking sheet.",
                    "Flatten each ball with a fork, creating a crisscross pattern.",
                    "Bake for 10-12 minutes, or until the edges are golden brown.",
                    "Allow to cool on the baking sheet for a few minutes before transferring to a wire rack to cool completely."
                },
                Ingredients = new List<string>
                {
                    "Peanut butter",
                    "Sugar",
                    "Brown sugar",
                    "Egg",
                    "Vanilla extract",
                    "Flour",
                    "Baking soda",
                    "Salt"
                },
                Image = "https://www.twopeasandtheirpod.com/wp-content/uploads/2022/10/Soft-Peanut-Butter-Cookies-16.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 10 }
                ]
            },
            new RecipeEntity
            {
                Title = "Creamy Tomato Basil Soup",
                Description = "A rich and creamy tomato soup with fresh basil, perfect for a comforting dinner.",
                CookingTime = new TimeSpan(0, 0, 45, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large pot, sauté onions and garlic until translucent.",
                    "Add canned tomatoes, vegetable broth, and fresh basil.",
                    "Simmer for 20-25 minutes.",
                    "Blend the soup until smooth using an immersion blender.",
                    "Stir in cream and season with salt and pepper.",
                    "Serve hot with crusty bread."
                },
                Ingredients = new List<string>
                {
                    "Onion",
                    "Garlic",
                    "Canned tomatoes",
                    "Vegetable broth",
                    "Fresh basil",
                    "Heavy cream",
                    "Salt",
                    "Pepper"
                },
                Image = "https://thefoodcharlatan.com/wp-content/uploads/2011/11/Easy-Homemade-Tomato-Soup-30-Minutes-4-650x975.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Berry Banana Smoothie",
                Description = "A refreshing smoothie made with mixed berries, banana, and yogurt, perfect for a quick and healthy breakfast.",
                CookingTime = new TimeSpan(0, 0, 5, 0),
                Servings = 2,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Place mixed berries, banana, yogurt, and milk in a blender.",
                    "Blend until smooth and creamy.",
                    "Pour into glasses and serve immediately."
                },
                Ingredients = new List<string>
                {
                    "Mixed berries (strawberries, blueberries, raspberries)",
                    "Banana",
                    "Greek yogurt",
                    "Milk (or a dairy-free alternative)"
                },
                Image = "https://iheartvegetables.com/wp-content/uploads/2022/09/Banana-Berry-Smoothie-featured-image-1-of-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 1 }
                ]
            },
            new RecipeEntity
            {
                Title = "Classic Pancakes",
                Description = "Fluffy and golden pancakes perfect for a traditional breakfast, served with maple syrup and fresh berries.",
                CookingTime = new TimeSpan(0, 0, 30, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a bowl, whisk together flour, sugar, baking powder, and salt.",
                    "In another bowl, whisk together milk, eggs, and melted butter.",
                    "Pour the wet ingredients into the dry ingredients and stir until just combined.",
                    "Heat a non-stick skillet over medium heat and lightly grease with butter or oil.",
                    "Pour batter onto the skillet to form pancakes.",
                    "Cook until bubbles form on the surface and the edges look set, then flip and cook until golden brown.",
                    "Serve warm with maple syrup and fresh berries."
                },
                Ingredients = new List<string>
                {
                    "Flour",
                    "Sugar",
                    "Baking powder",
                    "Salt",
                    "Milk",
                    "Eggs",
                    "Butter",
                    "Maple syrup",
                    "Fresh berries"
                },
                Image = "https://www.pamperedchef.com/iceberg/com/recipe/1307769-lg.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 8 }
                ]
            },
            new RecipeEntity
            {
                Title = "Green Smoothie Bowl",
                Description = "A nutritious and vibrant smoothie bowl packed with spinach, banana, and toppings of your choice.",
                CookingTime = new TimeSpan(0, 0, 10, 0),
                Servings = 2,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Blend spinach, banana, almond milk, and honey until smooth.",
                    "Pour the smoothie into bowls.",
                    "Top with granola, sliced fruits, chia seeds, and coconut flakes.",
                    "Serve immediately."
                },
                Ingredients = new List<string>
                {
                    "Spinach",
                    "Banana",
                    "Almond milk",
                    "Honey",
                    "Granola",
                    "Sliced fruits (such as kiwi, berries)",
                    "Chia seeds",
                    "Coconut flakes"
                },
                Image = "https://i2.wp.com/www.downshiftology.com/wp-content/uploads/2016/02/clean-green-smoothie-bowl-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Oatmeal with Fresh Berries",
                Description = "A warm and hearty bowl of oatmeal topped with fresh berries and a drizzle of honey.",
                CookingTime = new TimeSpan(0, 0, 10, 0),
                Servings = 2,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a pot, bring water or milk to a boil.",
                    "Stir in oats and reduce heat to a simmer.",
                    "Cook, stirring occasionally, until the oats are soft and creamy.",
                    "Divide the oatmeal into bowls and top with fresh berries and a drizzle of honey.",
                    "Serve warm."
                },
                Ingredients = new List<string>
                {
                    "Oats",
                    "Water or milk",
                    "Fresh berries (strawberries, blueberries, raspberries)",
                    "Honey"
                },
                Image = "https://naturallyella.com/wp-content/uploads/2015/04/Summer-Berry-Overnight-Oats-2.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Mediterranean Quinoa Salad",
                Description = "A vibrant and healthy salad with quinoa, cherry tomatoes, cucumber, olives, and feta cheese, dressed with lemon vinaigrette.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 4,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Cook quinoa according to package instructions and let it cool.",
                    "In a large bowl, combine cooked quinoa, cherry tomatoes, cucumber, olives, red onion, and feta cheese.",
                    "In a small bowl, whisk together olive oil, lemon juice, garlic, salt, and pepper.",
                    "Pour the dressing over the salad and toss to combine.",
                    "Garnish with fresh parsley before serving."
                },
                Ingredients = new List<string>
                {
                    "Quinoa",
                    "Cherry tomatoes",
                    "Cucumber",
                    "Olives",
                    "Red onion",
                    "Feta cheese",
                    "Olive oil",
                    "Lemon juice",
                    "Garlic",
                    "Salt",
                    "Pepper",
                    "Fresh parsley"
                },
                Image = null,
                Tags =
                [
                    new TagRecipeEntity { TagId = 4 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Easy Caprese Salad",
                Description = "A refreshing salad featuring the classic combination of tomatoes, mozzarella, and basil.",
                CookingTime = new TimeSpan(0, 0, 15, 0),
                Servings = 2,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Slice fresh tomatoes and mozzarella cheese.",
                    "Arrange tomato and mozzarella slices on a plate.",
                    "Drizzle with balsamic glaze and olive oil.",
                    "Sprinkle with salt, pepper, and fresh basil leaves.",
                },
                Ingredients = new List<string>
                {
                    "Fresh tomatoes",
                    "Fresh mozzarella cheese",
                    "Balsamic glaze",
                    "Olive oil",
                    "Fresh basil leaves"
                },
                Image = "https://www.iheartnaptime.net/wp-content/uploads/2020/07/Caprese-salad-2.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 1 }, 
                    new TagRecipeEntity { TagId = 4 },
                    new TagRecipeEntity { TagId = 7 }
                ],
            },
            new RecipeEntity
            {
                Title = "Speedy Breakfast Burritos",
                Description = "Delicious breakfast burritos packed with eggs, cheese, and your favorite toppings.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Scramble eggs in a skillet until cooked through.",
                    "Warm tortillas in a separate skillet or microwave.",
                    "Spoon scrambled eggs onto tortillas.",
                    "Add cheese, salsa, avocado, and any other desired toppings.",
                    "Roll up the burritos and serve.",
                },
                Ingredients = new List<string>
                {
                    "Eggs",
                    "Flour tortillas",
                    "Shredded cheese",
                    "Salsa",
                    "Avocado"
                },
                Image = "https://emilybites.com/wp-content/uploads/2021/06/Turkey-Sausage-Breakfast-Burritos-5b.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 1 }, 
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 7 }
                ],
            },
            new RecipeEntity
            {
                Title = "Simple Minestrone Soup",
                Description = "A hearty vegetable soup with beans and pasta, perfect for a quick and comforting meal.",
                CookingTime = new TimeSpan(0, 0, 30, 0),
                Servings = 6,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large pot, sauté onions, carrots, and celery until softened.",
                    "Add diced tomatoes, vegetable broth, beans, and pasta.",
                    "Simmer until pasta is cooked and vegetables are tender.",
                    "Season with salt, pepper, and Italian herbs.",
                    "Serve hot with crusty bread.",
                },
                Ingredients = new List<string>
                {
                    "Onion",
                    "Carrots",
                    "Celery",
                    "Diced tomatoes",
                    "Vegetable broth",
                    "Cannellini beans",
                    "Pasta",
                },
                Image = "https://cdn.loveandlemons.com/wp-content/uploads/2021/11/minestrone-soup.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 1 }, 
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 7 }
                ],
            },
                        new RecipeEntity
            {
                Title = "Caprese Salad",
                Description = "A simple and elegant Italian salad with fresh tomatoes, mozzarella cheese, basil, and a balsamic glaze.",
                CookingTime = new TimeSpan(0, 0, 10, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Slice tomatoes and mozzarella cheese into even rounds.",
                    "Arrange tomato and mozzarella slices on a platter, alternating between the two.",
                    "Tuck fresh basil leaves between the slices.",
                    "Drizzle with olive oil and balsamic glaze.",
                    "Sprinkle with salt and pepper before serving."
                },
                Ingredients = new List<string>
                {
                    "Tomatoes",
                    "Mozzarella cheese",
                    "Fresh basil",
                    "Olive oil",
                    "Balsamic glaze",
                    "Salt",
                    "Pepper"
                },
                Image = "https://whatsgabycooking.com/wp-content/uploads/2023/06/Dinner-Party-__-Traditional-Caprese-1-1200x800-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 4 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 1 }
                ]
            },
            new RecipeEntity
            {
                Title = "Tomato Basil Soup",
                Description = "A rich and creamy tomato soup flavored with fresh basil, perfect for a cozy meal.",
                CookingTime = new TimeSpan(0, 0, 30, 0),
                Servings = 4,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large pot, heat olive oil over medium heat.",
                    "Add chopped onions and garlic, and sauté until softened.",
                    "Add canned tomatoes, vegetable broth, and fresh basil leaves.",
                    "Bring to a boil, then reduce heat and simmer for 20 minutes.",
                    "Use an immersion blender to blend the soup until smooth.",
                    "Stir in heavy cream and season with salt and pepper.",
                    "Serve hot, garnished with fresh basil leaves."
                },
                Ingredients = new List<string>
                {
                    "Olive oil",
                    "Onions",
                    "Garlic",
                    "Canned tomatoes",
                    "Vegetable broth",
                    "Fresh basil leaves",
                    "Heavy cream",
                    "Salt",
                    "Pepper"
                },
                Image = null,
                Tags =
                [
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "One-Pan Lemon Herb Chicken",
                Description = "Juicy chicken breasts cooked with lemon and herbs in one pan for an easy weeknight dinner.",
                CookingTime = new TimeSpan(0, 0, 25, 0),
                Servings = 4,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Season chicken breasts with salt, pepper, and herbs.",
                    "Sear chicken in a hot skillet until golden brown.",
                    "Add lemon slices and chicken broth to the skillet.",
                    "Simmer until chicken is cooked through and sauce is reduced.",
                    "Serve with steamed vegetables or salad.",
                },
                Ingredients = new List<string>
                {
                    "Chicken breasts",
                    "Lemon",
                    "Chicken broth",
                    "Fresh herbs (rosemary or thyme)"
                },
                Image = "https://simply-delicious-food.com/wp-content/uploads/2019/05/lemon-herb-grilled-chicken-breast-3.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 1 }, 
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 6 },
                    new TagRecipeEntity { TagId = 9 },
                ],
            },
            new RecipeEntity
            {
                Title = "Easy Banana Bread",
                Description = "Moist and delicious banana bread that's simple to make and perfect for breakfast or snacking.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 8,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Mash ripe bananas in a large bowl.",
                    "Stir in melted butter, sugar, eggs, and vanilla extract.",
                    "Fold in flour, baking powder, and salt until just combined.",
                    "Pour batter into a greased loaf pan and bake until golden brown.",
                    "Allow to cool before slicing and serving.",
                },
                Ingredients = new List<string>
                {
                    "Ripe bananas",
                    "Butter",
                    "Sugar",
                    "Eggs",
                    "Vanilla extract",
                    "Flour",
                    "Baking powder",
                },
                Image = "https://natashaskitchen.com/wp-content/uploads/2018/05/Banana-Bread-Recipe-6.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 1 }, 
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 8 },
                ],
            },
            new RecipeEntity
            {
                Title = "Chicken Caesar Salad",
                Description = "A classic Caesar salad with grilled chicken, crisp romaine lettuce, croutons, Parmesan cheese, and a creamy Caesar dressing.",
                CookingTime = new TimeSpan(0, 0, 25, 0),
                Servings = 4,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Season chicken breasts with salt and pepper and grill until fully cooked. Let cool and slice.",
                    "In a large bowl, toss romaine lettuce with Caesar dressing until well coated.",
                    "Top with sliced grilled chicken, croutons, and grated Parmesan cheese.",
                    "Serve immediately."
                },
                Ingredients = new List<string>
                {
                    "Chicken breasts",
                    "Romaine lettuce",
                    "Caesar dressing",
                    "Croutons",
                    "Parmesan cheese",
                    "Salt",
                    "Pepper"
                },
                Image = "https://reciperunner.com/wp-content/uploads/2017/07/Grilled-Chicken-Caesar-Salad-Photograph.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 4 },
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Kale and Apple Salad",
                Description = "A refreshing salad with kale, sliced apples, cranberries, walnuts, and a honey mustard dressing.",
                CookingTime = new TimeSpan(0, 0, 15, 0),
                Servings = 4,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large bowl, massage kale with a pinch of salt until it becomes tender.",
                    "Add sliced apples, dried cranberries, and chopped walnuts.",
                    "In a small bowl, whisk together olive oil, apple cider vinegar, honey, Dijon mustard, salt, and pepper.",
                    "Pour the dressing over the salad and toss to combine.",
                    "Serve immediately."
                },
                Ingredients = new List<string>
                {
                    "Kale",
                    "Apples",
                    "Dried cranberries",
                    "Walnuts",
                    "Olive oil",
                    "Apple cider vinegar",
                    "Honey",
                    "Dijon mustard",
                    "Salt",
                    "Pepper"
                },
                Image = "https://cleanfoodcrush.com/wp-content/uploads/2022/05/energizing-Kale-Apple-Salad-recipe.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 4 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            
            new RecipeEntity
            {
                Title = "Shrimp Scampi Pasta",
                Description = "A classic Italian dish with succulent shrimp cooked in a garlic butter sauce, served over pasta.",
                CookingTime = new TimeSpan(0, 0, 25, 0),
                Servings = 4,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Cook pasta according to package instructions.",
                    "In a large skillet, melt butter and sauté garlic until fragrant.",
                    "Add shrimp and cook until pink and opaque.",
                    "Stir in lemon juice, white wine, and parsley.",
                    "Toss the cooked pasta with the shrimp and sauce.",
                    "Serve hot, garnished with extra parsley."
                },
                Ingredients = new List<string>
                {
                    "Pasta",
                    "Shrimp",
                    "Butter",
                    "Garlic",
                    "Lemon juice",
                    "White wine",
                    "Fresh parsley"
                },
                Image = "https://www.onceuponachef.com/images/2023/06/shrimp-scampi-with-pasta-4.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 1 }
                ]
            },
            new RecipeEntity
            {
                Title = "Vegetarian Tacos",
                Description = "Flavorful vegetarian tacos filled with spiced black beans, fresh vegetables, and avocado.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 4,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a skillet, heat olive oil and cook onions until translucent.",
                    "Add black beans, corn, and spices. Cook until heated through.",
                    "Warm tortillas in a separate skillet or microwave.",
                    "Assemble tacos with the bean mixture, diced tomatoes, lettuce, and avocado slices.",
                    "Serve with lime wedges and fresh cilantro."
                },
                Ingredients = new List<string>
                {
                    "Olive oil",
                    "Onion",
                    "Black beans",
                    "Corn",
                    "Spices (cumin, chili powder)",
                    "Tortillas",
                    "Diced tomatoes",
                    "Lettuce",
                    "Avocado",
                    "Lime wedges",
                    "Fresh cilantro"
                },
                Image = null,
                Tags =
                [
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Avocado Toast with Poached Egg",
                Description = "A simple and nutritious breakfast featuring creamy avocado spread on toast, topped with a perfectly poached egg.",
                CookingTime = new TimeSpan(0, 0, 15, 0),
                Servings = 2,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Toast the bread slices until golden brown.",
                    "Mash the avocado with a fork and season with salt, pepper, and lemon juice.",
                    "Spread the avocado mixture on the toasted bread.",
                    "Poach the eggs in simmering water for about 3-4 minutes until the whites are set and the yolks are runny.",
                    "Place a poached egg on top of each avocado toast.",
                    "Sprinkle with red pepper flakes and garnish with fresh herbs."
                },
                Ingredients = new List<string>
                {
                    "Bread slices",
                    "Avocado",
                    "Salt",
                    "Pepper",
                    "Lemon juice",
                    "Eggs",
                    "Red pepper flakes",
                    "Fresh herbs (such as parsley or cilantro)"
                },
                Image = "https://pinchofyum.com/wp-content/uploads/poached-egg-toast-square.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Butternut Squash Soup",
                Description = "A creamy and flavorful soup made with roasted butternut squash, perfect for a comforting meal.",
                CookingTime = new TimeSpan(0, 0, 45, 0),
                Servings = 4,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 400°F (200°C).",
                    "Cut the butternut squash in half, remove seeds, and place on a baking sheet.",
                    "Drizzle with olive oil, and season with salt and pepper.",
                    "Roast in the oven for 25-30 minutes, until tender.",
                    "In a large pot, sauté chopped onions and garlic in olive oil until softened.",
                    "Scoop the roasted squash into the pot and add vegetable broth.",
                    "Bring to a boil, then reduce heat and simmer for 10 minutes.",
                    "Use an immersion blender to blend the soup until smooth.",
                    "Stir in coconut milk and season with salt, pepper, and nutmeg.",
                    "Serve hot, garnished with a swirl of coconut milk and fresh herbs."
                },
                Ingredients = new List<string>
                {
                    "Butternut squash",
                    "Olive oil",
                    "Salt",
                    "Pepper",
                    "Onions",
                    "Garlic",
                    "Vegetable broth",
                    "Coconut milk",
                    "Nutmeg",
                    "Fresh herbs"
                },
                Image = "https://www.twopeasandtheirpod.com/wp-content/uploads/2022/11/Butternut-Squash-Soup-4.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            
            new RecipeEntity
            {
                Title = "Gluten-Free Banana Pancakes",
                Description = "Delicious and fluffy pancakes made with ripe bananas and gluten-free flour, perfect for a hearty breakfast.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 4,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large bowl, mash the ripe bananas until smooth.",
                    "Add eggs, milk, and vanilla extract, and whisk to combine.",
                    "In another bowl, mix the gluten-free flour, baking powder, and salt.",
                    "Add the dry ingredients to the banana mixture and stir until just combined.",
                    "Heat a non-stick skillet over medium heat and lightly grease with butter or oil.",
                    "Pour batter onto the skillet to form pancakes.",
                    "Cook until bubbles form on the surface and the edges look set, then flip and cook until golden brown.",
                    "Serve warm with maple syrup and fresh fruit."
                },
                Ingredients = new List<string>
                {
                    "Ripe bananas",
                    "Eggs",
                    "Milk",
                    "Vanilla extract",
                    "Gluten-free flour",
                    "Baking powder",
                    "Salt",
                    "Butter or oil",
                    "Maple syrup",
                    "Fresh fruit"
                },
                Image = "https://www.mamaknowsglutenfree.com/wp-content/uploads/2021/08/gluten-free-banana-pancakes-rc-1-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 6 },
                    new TagRecipeEntity { TagId = 3 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Chocolate Avocado Mousse",
                Description = "A rich and creamy chocolate mousse made with avocado, perfect for a healthy and indulgent treat.",
                CookingTime = new TimeSpan(0, 0, 15, 0),
                Servings = 4,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a food processor, combine ripe avocados, cocoa powder, honey, vanilla extract, and a pinch of salt.",
                    "Blend until smooth and creamy.",
                    "Divide the mousse into serving dishes and refrigerate for at least 30 minutes.",
                    "Serve chilled, garnished with fresh berries and a mint leaf."
                },
                Ingredients = new List<string>
                {
                    "Ripe avocados",
                    "Cocoa powder",
                    "Honey",
                    "Vanilla extract",
                    "Salt",
                    "Fresh berries",
                    "Mint leaf"
                },
                Image = "https://chocolatecoveredkatie.com/wp-content/uploads/2015/10/Avocado-Chocolate-Mousse-Recipe.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 9 },
                    new TagRecipeEntity { TagId = 7 }
                ]
            },
            new RecipeEntity
            {
                Title = "Gluten-Free Brownies",
                Description = "Fudgy and delicious brownies made with gluten-free flour, perfect for satisfying your chocolate cravings.",
                CookingTime = new TimeSpan(0, 0, 40, 0),
                Servings = 12,
                AuthorId = 1,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "In a large bowl, melt the butter and chocolate together.",
                    "Stir in sugar and vanilla extract.",
                    "Add eggs one at a time, mixing well after each addition.",
                    "Stir in gluten-free flour and salt until just combined.",
                    "Pour the batter into a greased baking dish.",
                    "Bake for 25-30 minutes or until a toothpick inserted in the center comes out clean.",
                    "Let cool before slicing and serving."
                },
                Ingredients = new List<string>
                {
                    "Butter",
                    "Chocolate",
                    "Sugar",
                    "Vanilla extract",
                    "Eggs",
                    "Gluten-free flour",
                    "Salt"
                },
                Image = "https://butternutbakeryblog.com/wp-content/uploads/2023/05/gluten-free-brownies-stacked.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 6 }
                ]
            },
            new RecipeEntity
            {
                Title = "Lemon Blueberry Cheesecake Bars",
                Description = "Creamy cheesecake bars with a zesty lemon flavor and sweet blueberries, on a graham cracker crust.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 9,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "In a bowl, combine crushed graham crackers, melted butter, and sugar. Press into the bottom of a baking dish.",
                    "In a large bowl, beat cream cheese, sugar, eggs, lemon juice, and lemon zest until smooth.",
                    "Pour the cheesecake mixture over the crust and scatter blueberries on top.",
                    "Bake for 30-35 minutes, or until the center is set.",
                    "Let cool completely before slicing into bars.",
                    "Refrigerate for at least 2 hours before serving."
                },
                Ingredients = new List<string>
                {
                    "Graham crackers",
                    "Butter",
                    "Sugar",
                    "Cream cheese",
                    "Eggs",
                    "Lemon juice",
                    "Lemon zest",
                    "Blueberries"
                },
                Image = "https://sallysbakingaddiction.com/wp-content/uploads/2022/08/lemon-blueberry-cheesecake-bars-1.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 3 }
                ]
            },
            new RecipeEntity
            {
                Title = "Chicken Noodle Soup",
                Description = "A classic and comforting chicken noodle soup made with tender chicken, vegetables, and egg noodles.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 6,
                AuthorId = 3,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "In a large pot, heat olive oil over medium heat.",
                    "Add chopped onions, carrots, and celery, and sauté until softened.",
                    "Add minced garlic and cook for another minute.",
                    "Pour in chicken broth and bring to a boil.",
                    "Add chicken breasts and cook until fully cooked, about 20 minutes.",
                    "Remove the chicken, shred it, and return it to the pot.",
                    "Add egg noodles and cook until tender.",
                    "Season with salt, pepper, and fresh parsley.",
                    "Serve hot, garnished with fresh parsley."
                },
                Ingredients = new List<string>
                {
                    "Olive oil",
                    "Onions",
                    "Carrots",
                    "Celery",
                    "Garlic",
                    "Chicken broth",
                    "Chicken breasts",
                    "Egg noodles",
                    "Salt",
                    "Pepper",
                    "Fresh parsley"
                },
                Image = "https://recipetineats.com/wp-content/uploads/2017/05/Chicken-Noodle-Soup-from-scratch_3.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 5 },
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Stuffed Bell Peppers",
                Description = "Colorful bell peppers stuffed with a savory mixture of ground beef, rice, tomatoes, and spices, baked to perfection.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 4,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 375°F (190°C).",
                    "Cut the tops off the bell peppers and remove the seeds and membranes.",
                    "In a large skillet, cook ground beef over medium heat until browned. Drain excess fat.",
                    "Add chopped onions, garlic, diced tomatoes, cooked rice, and seasonings to the skillet. Cook for 5-7 minutes.",
                    "Stuff each bell pepper with the beef mixture and place them in a baking dish.",
                    "Top with shredded cheese, if desired.",
                    "Bake for 30-35 minutes until the peppers are tender.",
                    "Serve hot, garnished with fresh parsley."
                },
                Ingredients = new List<string>
                {
                    "Bell peppers",
                    "Ground beef",
                    "Onions",
                    "Garlic",
                    "Diced tomatoes",
                    "Cooked rice",
                    "Salt",
                    "Pepper",
                    "Paprika",
                    "Shredded cheese (optional)",
                    "Fresh parsley"
                },
                Image = null,
                Tags =
                [
                    new TagRecipeEntity { TagId = 6 },
                    new TagRecipeEntity { TagId = 2 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Zucchini Noodles with Pesto",
                Description = "A light and refreshing dish of spiralized zucchini noodles tossed with homemade basil pesto.",
                CookingTime = new TimeSpan(0, 0, 20, 0),
                Servings = 2,
                AuthorId = 2,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Spiralize the zucchini into noodles using a spiralizer or julienne peeler.",
                    "In a food processor, combine basil leaves, garlic, pine nuts, Parmesan cheese, and olive oil. Blend until smooth.",
                    "In a large skillet, lightly sauté the zucchini noodles in olive oil for 2-3 minutes until just tender.",
                    "Remove from heat and toss with the pesto sauce.",
                    "Season with salt and pepper to taste.",
                    "Serve immediately, garnished with extra Parmesan cheese and fresh basil leaves."
                },
                Ingredients = new List<string>
                {
                    "Zucchini",
                    "Basil leaves",
                    "Garlic",
                    "Pine nuts",
                    "Parmesan cheese",
                    "Olive oil",
                    "Salt",
                    "Pepper",
                    "Fresh basil leaves"
                },
                Image = "https://www.twopeasandtheirpod.com/wp-content/uploads/2013/08/Zucchini-Noodles-with-Pesto-7.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 6 },
                    new TagRecipeEntity { TagId = 7 },
                    new TagRecipeEntity { TagId = 9 }
                ]
            },
            new RecipeEntity
            {
                Title = "Apple Crisp",
                Description = "A warm and comforting apple crisp with a crunchy oat topping, perfect for a cozy dessert.",
                CookingTime = new TimeSpan(0, 1, 0, 0),
                Servings = 6,
                AuthorId = 4,
                UpdatedAt = DateTime.UtcNow,
                Instructions = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "In a large bowl, toss apple slices with lemon juice, sugar, and cinnamon. Spread them in a baking dish.",
                    "In another bowl, combine oats, flour, brown sugar, and cinnamon.",
                    "Cut in the butter until the mixture resembles coarse crumbs.",
                    "Sprinkle the oat mixture over the apples.",
                    "Bake for 35-40 minutes, or until the topping is golden brown and the apples are tender.",
                    "Serve warm, optionally with vanilla ice cream or whipped cream."
                },
                Ingredients = new List<string>
                {
                    "Apples",
                    "Lemon juice",
                    "Sugar",
                    "Cinnamon",
                    "Oats",
                    "Flour",
                    "Brown sugar",
                    "Butter"
                },
                Image = "https://sallysbakingaddiction.com/wp-content/uploads/2020/10/apple-crisp-with-ice-cream-and-caramel.jpg",
                Tags =
                [
                    new TagRecipeEntity { TagId = 8 },
                    new TagRecipeEntity { TagId = 10 }
                ]
            }
        ];
    }

    private static TagEntity[] CreateSeedTagsData()
    {
        // We assume the ids will be in this same order.
        return
        [
            new TagEntity { Name = "Quick & Easy" },
            new TagEntity { Name = "Dinner" },
            new TagEntity { Name = "Breakfast" },
            new TagEntity { Name = "Salads" },
            new TagEntity { Name = "Soups" },
            new TagEntity { Name = "Gluten-Free" },
            new TagEntity { Name = "Vegetarian" },
            new TagEntity { Name = "Dessert" },
            new TagEntity { Name = "Healthy" },
            new TagEntity { Name = "Low-budget" }
        ];
    }
}