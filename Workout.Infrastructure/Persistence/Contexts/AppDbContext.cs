using Microsoft.EntityFrameworkCore;

namespace Workout.Infrastructure.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base()
    {
        
    }
}