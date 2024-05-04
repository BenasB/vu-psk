using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IIdentityService
{
    Task<IEnumerable<User>> GetAllAsync();
}

public class IdentityService(IHttpClientFactory httpClientFactory) : IIdentityService
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var httpClient = CreateIdentityClient();
        return await httpClient.GetFromJsonAsync<IEnumerable<User>>("users") ?? throw new InvalidOperationException();
    }
    
    private HttpClient CreateIdentityClient() => httpClientFactory.CreateClient("Identity.API");

}