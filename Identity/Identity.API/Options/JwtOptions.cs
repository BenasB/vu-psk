namespace Identity.API.Options;

public class JwtOptions
{
    public const string SectionName = "JWT";

    public string Key { get; set; } = String.Empty;
    public string Issuer { get; set; } = String.Empty;  
    public string Audience {  get; set; } = String.Empty;
}
