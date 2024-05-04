using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IUserService
{
    Task<User[]> GetAllAsync();
}

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<User[]> GetAllAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<User[]>("users");
    }
}