using System.Data;
using System.Net;
using Recipes.Public;

namespace FrontEnd.Web.Services;

public interface IRecipeService
{
    Task<PaginatedResponse<Recipe>> GetAllAsync(string? searchTerm = null, IEnumerable<Tag>? tags = null, (int Skip, int Top)? pagination = null);
    Task<PaginatedResponse<Recipe>> GetAllByAuthorAsync(int authorId);
    Task<Recipe?> GetByIdAsync(int id);
    Task<bool> DeleteByIdAsync(int id);
    Task<Recipe> CreateAsync(RecipeCreateDTO request);
    Task UpdateAsync(int id, RecipeUpdateDTO request);
    Task<PaginatedResponse<Tag>> GetAllTagsAsync();
    Task<bool> DeleteTagByIdAsync(int tagId);
    Task<Tag> CreateTagAsync(TagCreateUpdateDTO request);
}

public class RecipeService : IRecipeService
{
    private readonly HttpClient _httpClient;
    
    public RecipeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:RecipesApiBaseUrl")!);
    }
    
    public async Task<PaginatedResponse<Recipe>> GetAllAsync(string? searchTerm = null, IEnumerable<Tag>? tags = null, (int Skip, int Top)? pagination = null)
    {
        var queryParameters = new Dictionary<string, string?>();

        if (searchTerm != null)
            queryParameters["title"] = searchTerm;
        
        if (tags != null)
            queryParameters["csvTags"] = string.Join(",", tags.Select(t => t.Id));

        if (pagination != null)
        {
            queryParameters["skip"] = pagination.Value.Skip.ToString();
            queryParameters["top"] = pagination.Value.Top.ToString();
        }
        
        return await _httpClient.GetFromJsonAsync<PaginatedResponse<Recipe>>("recipes" + QueryString.Create(queryParameters)) ?? throw new InvalidOperationException();
    }

    public async Task<PaginatedResponse<Recipe>> GetAllByAuthorAsync(int authorId)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResponse<Recipe>>($"recipes?authorId={authorId}") ?? throw new InvalidOperationException();
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

    public async Task UpdateAsync(int id, RecipeUpdateDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync($"recipes/{id}", request);
        if (response.StatusCode == HttpStatusCode.Conflict)
            throw new DBConcurrencyException();
    }

    public async Task<PaginatedResponse<Tag>> GetAllTagsAsync()
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResponse<Tag>>("tags") ?? throw new InvalidOperationException();
    }

    public async Task<bool> DeleteTagByIdAsync(int tagId)
    {
        var response = await _httpClient.DeleteAsync($"tags/{tagId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Tag> CreateTagAsync(TagCreateUpdateDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("tags", request);
        return await response.Content.ReadFromJsonAsync<Tag>() ?? throw new InvalidOperationException();
    }
}