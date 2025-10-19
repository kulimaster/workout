using MediatR;
using Workout.Application.Contracts.Persistence;
using Workout.Domain.Entities;

namespace Workout.Application.Exercises.Queries.GetExercises;

internal sealed class GetExercisesQueryHandler(IExerciseRepository repository) : IRequestHandler<GetExercisesQuery, IEnumerable<Exercise>>
{
    public async Task<IEnumerable<Exercise>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}