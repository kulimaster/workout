using MediatR;
using Workout.Application.Exercises.Commands.CreateExercise;

namespace Workout.Application.Exercises.Commands.UpdateExercise;

public sealed record UpdateExerciseCommand(Guid Id, ExerciseDto Exercise) : IRequest<Unit>;