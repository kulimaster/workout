using Workout.Domain.Enums;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed class CreateExerciseDto
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; }

    public MuscleGroup PrimaryMuscleGroup { get; init; }

    public EquipmentType Equipment { get; init; }

    public List<string> MediaUrls { get; init; } = new();
}