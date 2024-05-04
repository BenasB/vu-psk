using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Recipes.Public;

namespace FrontEnd.Web.Services;

public interface IRecipeService
{
    Task<Recipe[]> GetAllAsync();
}

public class RecipeService : IRecipeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RecipeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Recipe[]> GetAllAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<Recipe[]>("recipes");
    }
}