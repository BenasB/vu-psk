using Recipes.Public;

namespace FrontEnd.Web.Services;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllAsync();
}

public class RecipeService(IHttpClientFactory httpClientFactory) : IRecipeService
{
    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        using var httpClient = httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<IEnumerable<Recipe>>("recipes") ?? throw new InvalidOperationException();
    }
}