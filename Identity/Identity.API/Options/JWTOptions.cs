namespace Identity.API.Options;

public class JWTOptions
{
    public const string JWT = "JWT";

    public string Key { get; set; } = String.Empty;
    public string Issuer { get; set; } = String.Empty;  
    public string Audience {  get; set; } = String.Empty;
}
