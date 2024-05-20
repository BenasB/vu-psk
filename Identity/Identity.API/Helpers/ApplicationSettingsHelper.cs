namespace Identity.API.Helpers;

public class ApplicationSettingsHelper
{
    public static IConfiguration Settings { get; private set; }
    public static void Initialize(IConfiguration settings)
    {
        Settings = settings;
    }
}
