using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IIdentityService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
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
        var allUsers = (await GetAllAsync()).ToList();
        return id >= 0 && id < allUsers.Count ? allUsers[id] : null;
    }
}