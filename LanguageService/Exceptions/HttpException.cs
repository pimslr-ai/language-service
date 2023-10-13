namespace LanguageService.Exceptions;

public abstract class HttpException : Exception
{
    public readonly int StatusCode;
    public new readonly string? Message;
    public readonly string? FriendlyMessage;

    protected HttpException(int statusCode, string? message = null, string? friendlyMessage = null) 
    {
        StatusCode = statusCode;
        Message = message;
        FriendlyMessage = friendlyMessage;
    }
}
