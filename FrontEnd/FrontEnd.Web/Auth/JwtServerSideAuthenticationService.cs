using BitzArt.Blazor.Auth;
using FrontEnd.Web.Services;
using Identity.Public;

namespace FrontEnd.Web.Auth;

// ReSharper disable once ClassNeverInstantiated.Global
public class JwtServerSideAuthenticationService(IIdentityService identityService)
    : ServerSideAuthenticationService<UserLoginRequest, UserCreateRequest>
{
    protected override async Task<AuthenticationResult> GetSignInResultAsync(UserLoginRequest signInPayload)
    {
        var token = await identityService.LogIn(signInPayload);

        return AuthenticationResult.Success(new JwtPair
        {
            AccessToken = token
        });
    }

    protected override async Task<AuthenticationResult> GetSignUpResultAsync(UserCreateRequest signUpPayload)
    {
        var token = await identityService.Register(signUpPayload);

        return AuthenticationResult.Success(new JwtPair
        {
            AccessToken = token
        });
    }
}