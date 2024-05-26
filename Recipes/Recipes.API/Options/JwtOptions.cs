namespace Recipes.API.Options;

public class JwtOptions
{
    public const string SectionName = "JWT";

    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience {  get; init;  }
}
