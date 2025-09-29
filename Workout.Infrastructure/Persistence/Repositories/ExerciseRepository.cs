using Microsoft.EntityFrameworkCore;
using Workout.Application.Contracts.Persistence;
using Workout.Domain.Entities;
using Workout.Shared.Domain;

namespace Workout.Infrastructure.Persistence.Repositories;

public class ExerciseRepository(DbContext context) : RepositoryBase<Exercise>(context), IExerciseRepository
{
    
}