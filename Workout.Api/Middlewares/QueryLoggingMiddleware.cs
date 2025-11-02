using Serilog;
using Serilog.Context;

namespace Workout.Api.Middlewares;

public class QueryLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        // Převod query params na slovník
        var queryParams = context.Request.Query
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        // Volitelně můžeš filtrovat nebo maskovat citlivé parametry
        var filteredQuery = queryParams
            .Where(kvp => kvp.Key.ToLower() != "password" && kvp.Key.ToLower() != "token")
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        var requestPath = context.Request.Path.ToString();

        Log.ForContext("QueryParams", filteredQuery, destructureObjects: true)
            .ForContext<QueryLoggingMiddleware>()
            .Information("Request started url: {RequestPath}", requestPath);

        await next(context);


        /*// Push do LogContext, aby se props objevily v logu
        using (LogContext.PushProperty("QueryParams", filteredQuery))
        {
            //Log.ForContext<QueryLoggingMiddleware>().Information("Getting exercises");
            await _next(context);
        }*/
    }
}