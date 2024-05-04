using Identity.Public;

namespace FrontEnd.Web.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
}

public class UserService(IHttpClientFactory httpClientFactory) : IUserService
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var httpClient = CreateIdentityClient();
        return await httpClient.GetFromJsonAsync<IEnumerable<User>>("users") ?? throw new InvalidOperationException();
    }
    
    private HttpClient CreateIdentityClient() => httpClientFactory.CreateClient("Identity.API");

}