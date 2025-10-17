using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities;

namespace Workout.Infrastructure.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base()
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}