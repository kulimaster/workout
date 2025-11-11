using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Workout.Api.Shared;
using Workout.Shared.Exceptions;

namespace Workout.Api.Filters;

internal sealed class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Log.ForContext<GlobalExceptionFilter>().Error("Error {@Exception}", context.Exception);
        var problemDetails = new CustomValidationProblemDetails
        {
            Title = "An unexpected error occurred",
            Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = GetStatusCode(context)
        };

        context.ExceptionHandled = true;
    }

    private static int GetStatusCode(ExceptionContext context)
    {
        return context.Exception switch
        {
            ValidationErrorException => StatusCodes.Status400BadRequest,
            BusinessErrorException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}