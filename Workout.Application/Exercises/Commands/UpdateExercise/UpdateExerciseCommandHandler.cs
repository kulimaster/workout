using MediatR;
using Workout.Application.Contracts.Persistence;

namespace Workout.Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommandHandler(IExerciseRepository repository) : IRequestHandler<UpdateExerciseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateExerciseCommand input, CancellationToken cancellationToken)
    {
        var exercise = repository.GetByIdAsync(input.Id).Result;
        if (exercise is null)
        {
            //throw new BusinessErrorException("Exercise {id} not found", input.Id);
        }

        exercise.Update(input.Exercise.Name, input.Exercise.Description, input.Exercise.PrimaryMuscleGroup,
            input.Exercise.Equipment);
        
        await repository.UpdateAsync(exercise);
        return Unit.Value;
    }
}