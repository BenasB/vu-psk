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
        using var httpClient = httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<IEnumerable<User>>("users") ?? throw new InvalidOperationException();
    }
}