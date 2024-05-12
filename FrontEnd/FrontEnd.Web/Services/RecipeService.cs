using Recipes.Public;

namespace FrontEnd.Web.Services;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
}

public class RecipeService : IRecipeService
{
    private readonly HttpClient _httpClient;
    
    public RecipeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:RecipesApiBaseUrl")!);
    }
    
    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Recipe>>("recipes") ?? throw new InvalidOperationException();
    }
    
    public async Task<Recipe?> GetByIdAsync(int id)
    {
        return (await GetAllAsync()).FirstOrDefault(r => r.Id == id);
    }
}