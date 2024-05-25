namespace Recipes.API;

using Microsoft.AspNetCore.HttpLogging;
using System.IdentityModel.Tokens.Jwt;
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

        var tokenPayload = GetJWTTokenPayload(context);

        var requestLog = new StringBuilder();
        requestLog.AppendLine("");
        requestLog.AppendLine($"HTTP {request.Method} {request.Path}");
        requestLog.AppendLine($"User: {tokenPayload}");
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

    private string? GetJWTTokenPayload(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            return string.Empty;
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        var tokenPayload = jsonToken?.Payload.SerializeToJson();

        return tokenPayload;
    }
}
