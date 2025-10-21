namespace Workout.Api.Middlewares;

public class TraceIdHeaderMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers["X-Trace-Id"] = context.TraceIdentifier;
        await next(context);
    }
}
