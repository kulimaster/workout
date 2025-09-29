using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Workout.Infrastructure.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<YourDbContext>(options =>
        //     options.UseSqlServer("YourConnectionString"));

        // services.AddScoped<IExerciseRepository, ExerciseRepository>();
        // services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        // Add other repositories as needed

        return services;
    }
    
}