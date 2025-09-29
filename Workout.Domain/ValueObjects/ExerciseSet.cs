using Workout.Shared.Domain;

namespace Workout.Domain.ValueObjects;

public class ExerciseSet
{
    public int Repetitions { get; private set; }
    public double Weight { get; private set; } // v kg

    public ExerciseSet(int repetitions, double weight)
    {
        if (repetitions <= 0) throw new DomainException("Repetitions must be > 0.");
        if (weight < 0) throw new DomainException("Weight cannot be negative.");

        Repetitions = repetitions;
        Weight = weight;
    }
}