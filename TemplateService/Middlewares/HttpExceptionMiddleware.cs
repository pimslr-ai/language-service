using System.Text.Json;
using TemplateService.Exceptions;
using TemplateService.Middlewares.Models;

namespace TemplateService.Middlewares;

public class HttpExceptionMiddleware
{
    private readonly RequestDelegate next;

    public HttpExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (HttpException httpException)
        {
            var res = context.Response;
            res.StatusCode = httpException.StatusCode;
            res.ContentType = "application/json; charset=utf-8";
            
            var body = new HttpExceptionResponse
            {
                Message = httpException?.Message, 
                FriendlyMessage = httpException?.FriendlyMessage, 
                StatusCode = httpException.StatusCode,
            };
            
            await res.WriteAsync(JsonSerializer.Serialize(body));
        }
        catch (Exception)
        {
            var res = context.Response;
            res.StatusCode = 500;
            res.ContentType = "application/json; charset=utf-8";
            
            var body = new HttpExceptionResponse
            {
                Message = "Internal server error occured.", 
                FriendlyMessage = "Sorry but your request could not be processed...",
                StatusCode = 500,
            };

            await res.WriteAsync(JsonSerializer.Serialize(body));
            
            throw;
        }
    }
}
