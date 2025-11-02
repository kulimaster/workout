using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Workout.Infrastructure.Persistence;

namespace Workout.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

}