using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MovieEF.Domain.Commons;
using MovieEF.Service.Exceptions;

namespace MovieEF.Service.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await _next(context);
        }
        catch (HttpStatusCodeException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
    {
        context.Response.ContentType = "application/json";
        var result = new ErrorDetails
        {
            Message = exception.Message,
            StatusCode = exception.StatusCode
        };
        context.Response.StatusCode = exception.StatusCode;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var result = new ErrorDetails
        {
            Message = exception.ToString(),
            StatusCode = 500
        };
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}