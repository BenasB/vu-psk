using Recipes.Business.Exceptions;

namespace Recipes.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        context.Response.ContentType = "text/plain";
        return context.Response.WriteAsync(exception.Message);
    }
}