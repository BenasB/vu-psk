namespace Recipes.API;

using Microsoft.AspNetCore.HttpLogging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next,
    ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
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
        var userRole = context.User.FindFirstValue(ClaimTypes.Role);

        var requestLog = new StringBuilder();
        requestLog.AppendLine("");
        requestLog.AppendLine($"{DateTime.UtcNow}");
        requestLog.AppendLine("");
        requestLog.AppendLine($"HTTP {request.Method} {request.Path}");
        requestLog.AppendLine($"User Id: {userId}");
        requestLog.AppendLine($"User Role: {userRole}");
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
        File.AppendAllText("log.txt", content);
    }

}
