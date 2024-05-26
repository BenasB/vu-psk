using Recipes.Public;

namespace FrontEnd.Web.Services;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllAsync(string? searchTerm = null, IEnumerable<Tag>? tags = null);
    Task<IEnumerable<Recipe>> GetAllByAuthorAsync(int authorId);
    Task<Recipe?> GetByIdAsync(int id);
    Task<bool> DeleteByIdAsync(int id);
    Task<Recipe> CreateAsync(RecipeCreateDTO request);
    Task UpdateAsync(int id, RecipeUpdateDTO request);
    Task<IEnumerable<Tag>> GetAllTagsAsync();
}

public class RecipeService : IRecipeService
{
    private readonly HttpClient _httpClient;
    
    public RecipeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:RecipesApiBaseUrl")!);
    }
    
    public async Task<IEnumerable<Recipe>> GetAllAsync(string? searchTerm = null, IEnumerable<Tag>? tags = null)
    {
        var requestUri = (searchTerm, tags) switch
        {
            (null, null) => "recipes",
            (var nonNullSearchTerm, null) => $"recipes?title={nonNullSearchTerm}",
            (null, var nonNullTags) => $"recipes?csvTags={string.Join(",", nonNullTags.Select(t => t.Id))}",
            var (nonNullSearchTerm, nonNullTags) => $"recipes?title={nonNullSearchTerm}&csvTags={string.Join(",", nonNullTags.Select(t => t.Id))}"
        };
        return await _httpClient.GetFromJsonAsync<IEnumerable<Recipe>>(requestUri) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<Recipe>> GetAllByAuthorAsync(int authorId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Recipe>>($"recipes?authorId={authorId}") ?? throw new InvalidOperationException();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    { 
        var response = await _httpClient.GetAsync($"recipes/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Recipe>();
    }
    
    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"recipes/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Recipe> CreateAsync(RecipeCreateDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("recipes", request);
        return await response.Content.ReadFromJsonAsync<Recipe>() ?? throw new InvalidOperationException();
    }

    public Task UpdateAsync(int id, RecipeUpdateDTO request)
    {
        return _httpClient.PutAsJsonAsync($"recipes/{id}", request);
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Tag>>("tags") ?? throw new InvalidOperationException();
    }
}