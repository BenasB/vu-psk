namespace Recipes.Business.Exceptions;

public class HttpException : Exception
{
    public int StatusCode { get; set; }

    public HttpException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
