using Workout.Domain.Enums;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed class CreateExerciseDto
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; }

    public MuscleGroup PrimaryMuscleGroup { get; init; }

    public List<EquipmentType> Equipment { get; init; } = new();

    // Pro jednoduchost zatím budeme přidávat jen URL médií
    public List<string> MediaUrls { get; init; } = new();
}