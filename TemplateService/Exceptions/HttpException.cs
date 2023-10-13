namespace TemplateService.Exceptions;

public abstract class HttpException : Exception
{
    public new readonly string? Message;
    public readonly string? FriendlyMessage;
    public readonly int StatusCode;

    protected HttpException(int statusCode, string message = "", string friendlyMessage = "") 
    {
        Message = message;
        StatusCode = statusCode;
        FriendlyMessage = friendlyMessage;
    }
}
