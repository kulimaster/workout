using System.Text.Json;

namespace Workout.Api.Middlewares;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorCode = "Gloval Error message";
            var json = JsonSerializer.Serialize(new
            {
                error = e.Message,
                code = errorCode
            });
            await context.Response.WriteAsync(json);

        }
    }
}