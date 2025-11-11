using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Workout.Api.Shared;

namespace Workout.Api.Filters;

internal sealed class ValidateModelActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var response = new CustomValidationProblemDetails(context.ModelState);
            response.Status = StatusCodes.Status400BadRequest;
            response.Title = "One or more validation errors occurred.";
            response.Instance = context.HttpContext.Request.Path;
            response.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";

            context.Result = new BadRequestObjectResult(response);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}