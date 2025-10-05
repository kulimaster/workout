using Microsoft.EntityFrameworkCore;
using Workout.Application.Contracts.Persistence;
using Workout.Domain.Entities;
using Workout.Infrastructure.Persistence.Contexts;
using Workout.Shared.Domain;

namespace Workout.Infrastructure.Persistence.Repositories;

public class ExerciseRepository(AppDbContext context) : RepositoryBase<Exercise>(context), IExerciseRepository
{
    
}