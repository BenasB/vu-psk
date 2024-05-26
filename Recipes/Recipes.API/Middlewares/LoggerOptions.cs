namespace Recipes.API.Middlewares;

public class LoggerOptions
{
    public const string SectionName = "Logger";
    public required bool IsEnabled { get; set; }
    public required bool LogToFile { get; set; }
    public required string LogFilePath { get; set; }
}
