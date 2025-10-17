using Microsoft.AspNetCore.Mvc;

namespace Workout.Api.Filters;

public static class FilterExtensions
{
    public static void AddFilters(this MvcOptions options)
    {
        options.Filters.Add<ValidateModelActionFilter>();
        options.Filters.Add<GlobalExceptionFilter>();
    }
}