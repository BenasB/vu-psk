namespace Recipes.API.Middlewares;

using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    private LoggerOptions _options;

    public LoggingMiddleware(RequestDelegate next,
    ILogger<LoggingMiddleware> logger,
    IOptionsMonitor<LoggerOptions> options
    )
    {
        _next = next;
        _logger = logger;

        options.OnChange(option =>
        {
            _options = option;
        });
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(!_options.IsEnabled)
        {
            await _next(context);
            return;
        }

        string requestLog = GetRequestLog(context);
        Log(requestLog);

        await _next(context);

        string responseLog = GetResponseLog(context);
        Log(responseLog);
    }

    private string GetRequestLog(HttpContext context)
    {
        var request = context.Request;
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userRoles = context.User.FindAll(ClaimTypes.Role).Select(x => x.Type);

        var requestLog = new StringBuilder();
        requestLog.AppendLine("");
        requestLog.AppendLine($"{DateTime.UtcNow}");
        requestLog.AppendLine("");
        requestLog.AppendLine($"HTTP {request.Method} {request.Path}");
        requestLog.AppendLine($"User Id: {userId}");
        requestLog.AppendLine($"User Roles: {string.Join(", ", userRoles)}");
        requestLog.AppendLine($"Host: {request.Host}");
        requestLog.AppendLine($"Content-Type: {request.ContentType}");
        requestLog.AppendLine($"Content-Length: {request.ContentLength}");

        return requestLog.ToString();
    }

    private string GetResponseLog(HttpContext context)
    {
        var response = context.Response;

        var responseLog = new StringBuilder();
        responseLog.AppendLine($"\nStatus Code: {response.StatusCode}");
        responseLog.AppendLine($"Method Information: {context.GetEndpoint()?.DisplayName}");
        responseLog.AppendLine($"Content-Type: {response.ContentType}");
        responseLog.AppendLine($"Content-Length: {response.ContentLength}");
        responseLog.AppendLine("");
        responseLog.AppendLine($"=========================");

        return responseLog.ToString();
    }

    private void Log(string content)
    {
        _logger.LogInformation(content);

        if(_options.LogToFile)
            File.AppendAllText(_options.LogFilePath, content);
    }

}
