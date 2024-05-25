using BitzArt.Blazor.Auth;
using Identity.Public;

namespace FrontEnd.Web;

public class SampleServerSideAuthenticationService : ServerSideAuthenticationService<UserLoginRequest, UserCreateRequest>
{
    protected override Task<AuthenticationResult> GetSignInResultAsync(UserLoginRequest signInPayload)
    {
        var jwtPair = CreateSampleJwtPair(); // Replace this with your actual authentication logic

        var authResult = AuthenticationResult.Success(jwtPair);

        return Task.FromResult(authResult);
    }

    protected override Task<AuthenticationResult> GetSignUpResultAsync(UserCreateRequest signUpPayload)
    {
        var jwtPair = CreateSampleJwtPair(); // Replace this with your actual authentication logic

        var authResult = AuthenticationResult.Success(jwtPair);

        return Task.FromResult(authResult);
    }

    public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
    {
        var jwtPair = CreateSampleJwtPair(); // Replace this with your actual authentication logic

        var authResult = AuthenticationResult.Success(jwtPair);

        return Task.FromResult(authResult);
    }

    private JwtPair CreateSampleJwtPair()
    {
        return new JwtPair
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkpvaG4gRG9lIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOjAsImlhdCI6MTUxNjIzOTAyMiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiam9obi5kb2VAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbInVzZXIiLCJhZG1pbiJdfQ.dcvFi5tI48RPXLXgiyZHiXztMBrQxtRQWqAoz6cxNcM",
            RefreshToken = "my-refresh-token"
        };
    }
}