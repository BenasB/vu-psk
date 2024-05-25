using Microsoft.AspNetCore.Components.Authorization;

namespace FrontEnd.Web.Auth;

public class AuthenticationDelegatingHandler(AuthenticationStateProvider authenticationStateProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var token = authState.User.FindFirst("AccessToken")?.Value; // Added by BitzArt.Blazor.Auth.Server
        if (token != null)
            request.Headers.Add("Authorization", $"Bearer {token}");

        return await base.SendAsync(request, cancellationToken);
    }
}