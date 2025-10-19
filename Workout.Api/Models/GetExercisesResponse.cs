using Workout.Api.Shared;
using Workout.Domain.Entities;

namespace Workout.Api.Models;

public class GetExercisesResponse : PaginationResponse<List<CreateExerciseRequest>>
{
    public static GetExercisesResponse FromDomain(IEnumerable<Exercise> exercises)
    {
        var exerciseList = exercises.Select(e => new CreateExerciseRequest
        {
            Name = e.Name,
            Description = e.Description,
            PrimaryMuscleGroup = e.PrimaryMuscleGroup,
            Equipment = e.Equipment
        }).ToList();

        return new GetExercisesResponse
        {
            Data = exerciseList
        };
    }
}