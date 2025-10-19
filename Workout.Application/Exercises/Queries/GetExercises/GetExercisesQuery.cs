using MediatR;
using Workout.Domain.Entities;

namespace Workout.Application.Exercises.Queries.GetExercises;

public sealed record GetExercisesQuery() : IRequest<IEnumerable<Exercise>>;