using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Workout.Application.Common;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Logování na začátku
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Handling {RequestName} - {@Request}", requestName, request);

        // Měření času
        var stopwatch = Stopwatch.StartNew();

        // Volání dalšího behavior nebo samotného handleru
        var response = await next();

        stopwatch.Stop();

        // Logování na konci
        _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms - {@Response}",
            requestName, stopwatch.ElapsedMilliseconds, response);

        return response;
    }
}