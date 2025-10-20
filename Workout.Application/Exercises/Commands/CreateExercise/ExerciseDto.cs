using Workout.Domain.Enums;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed record ExerciseDto(
    string Name,
    string? Description,
    MuscleGroup PrimaryMuscleGroup,
    EquipmentType Equipment,
    List<string> MediaUrls
);