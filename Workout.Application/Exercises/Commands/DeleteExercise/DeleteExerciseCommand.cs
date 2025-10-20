using MediatR;

namespace Workout.Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(Guid Id) : IRequest<Unit>;