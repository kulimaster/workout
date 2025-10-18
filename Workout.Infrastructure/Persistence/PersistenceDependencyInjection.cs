using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workout.Application.Contracts.Persistence;
using Workout.Infrastructure.Configs;
using Workout.Infrastructure.Persistence.Contexts;
using Workout.Infrastructure.Persistence.Repositories;

namespace Workout.Infrastructure.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var pgConfig = sp.GetRequiredService<PostgresConfig>();
            options.UseNpgsql(pgConfig.BuildConnectionString());
        });
        
        return services;
    }
}