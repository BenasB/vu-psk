using System.Net;
using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IIdentityService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<string> LogIn(UserLoginRequest request);
    Task<string> Register(UserCreateRequest request);
}

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    
    public IdentityService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:IdentityApiBaseUrl")!);
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("users") ?? throw new InvalidOperationException();
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"users/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<User>();
    }
    
    public async Task<string> LogIn(UserLoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"login", request);
        // TODO: Handle failed login
        return await response.Content.ReadAsStringAsync() ?? throw new InvalidOperationException();
    }

    public async Task<string> Register(UserCreateRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"register", request);
        // TODO: Handle failed register
        return await response.Content.ReadAsStringAsync() ?? throw new InvalidOperationException();
    }
}