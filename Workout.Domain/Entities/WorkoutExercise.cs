using Workout.Domain.ValueObjects;
using Workout.Shared.Domain;

namespace Workout.Domain.Entities;

public class WorkoutExercise : BaseEntity
{
    public Guid WorkoutId { get; private set; }
    public Guid ExerciseId { get; private set; } // odkaz na knihovnu cviků
    public string Name { get; private set; }

    private readonly List<ExerciseSet> _sets = new();
    public IReadOnlyCollection<ExerciseSet> Sets => _sets.AsReadOnly();

    private WorkoutExercise() { } // EF Core

    public WorkoutExercise(Guid workoutId, Exercise exercise)
    {
        WorkoutId = workoutId;
        ExerciseId = exercise.Id;
        Name = exercise.Name;
    }

    public void AddSet(ExerciseSet set)
    {
        _sets.Add(set);
    }
}