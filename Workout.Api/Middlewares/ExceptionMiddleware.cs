using System.Text.Json;
using Serilog;

namespace Workout.Api.Middlewares;

internal sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Log.ForContext<ExceptionMiddleware>().Error("Generic Error {@Exception}", e);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new
            {
                error = e.Message,
                code = "Global Error message"
            });

            await context.Response.WriteAsync(json);
        }
    }
}
