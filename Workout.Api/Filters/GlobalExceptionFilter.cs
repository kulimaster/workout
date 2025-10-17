using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Workout.Shared.Exceptions;

namespace Workout.Api.Filters;

internal sealed class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Detail = context.Exception.Message //todo v produkci obvykle nahradíš generickou hláškou
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
            BusinessErrorException   => StatusCodes.Status422UnprocessableEntity,
            _                   => StatusCodes.Status500InternalServerError
        };
    }
}