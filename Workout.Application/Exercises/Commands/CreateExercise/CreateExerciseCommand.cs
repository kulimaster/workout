using MediatR;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(ExerciseDto Exercise) : IRequest<Guid>;