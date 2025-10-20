using MediatR;
using Workout.Application.Contracts.Persistence;

namespace Workout.Application.Exercises.Commands.DeleteExercise;

public class DeleteExerciseCommandHandler(IExerciseRepository repository) : IRequestHandler<DeleteExerciseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteByIdAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}