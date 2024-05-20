namespace Identity.Public;

public class UserCreateRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
}