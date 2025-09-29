using Workout.Shared.Domain;

namespace Workout.Domain.Entities;

public class Workout : BaseEntity
{
    public DateTime Date { get; private set; } = DateTime.UtcNow;
    public string? Notes { get; private set; }

    private readonly List<WorkoutExercise> _exercises = new();
    public IReadOnlyCollection<WorkoutExercise> Exercises => _exercises.AsReadOnly();

    private Workout() { } // EF Core

    public Workout(string? notes = null)
    {
        Notes = notes;
    }

    public void AddExercise(WorkoutExercise exercise)
    {
        _exercises.Add(exercise);
    }
}