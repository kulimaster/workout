using MediatR;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(CreateExerciseDto Exercise) : IRequest<Guid>;