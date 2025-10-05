using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Workout.Shared.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddConfigSingleton<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where T : class
    {
        var configInstance = configuration.GetSection(sectionName).Get<T>() 
                             ?? throw new Exception($"Configuration section '{sectionName}' not found or could not be bound to type {typeof(T).FullName}.");
        services.AddSingleton(configInstance);
        return services;
    }
}