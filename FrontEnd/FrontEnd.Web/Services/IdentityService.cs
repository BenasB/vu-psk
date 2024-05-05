using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IIdentityService
{
    Task<IEnumerable<User>> GetAllAsync();
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
}