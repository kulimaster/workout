using FluentValidation;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed class CreateExerciseCommandValidator 
    : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(x => x.Exercise).NotNull();

        RuleFor(x => x.Exercise.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Exercise.PrimaryMuscleGroup)
            .IsInEnum();

        RuleFor(x => x.Exercise.Equipment)
            .IsInEnum();

        RuleForEach(x => x.Exercise.MediaUrls)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Media URL must be valid absolute URL.");
    }
}
