using Microsoft.Extensions.DependencyInjection;

namespace Workout.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
    
}