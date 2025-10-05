using MediatR;
using Workout.Application.Contracts.Persistence;
using Workout.Domain.Entities;
using Workout.Domain.ValueObjects;

namespace Workout.Application.Exercises.Commands.CreateExercise;

public sealed class CreateExerciseCommandHandler 
    : IRequestHandler<CreateExerciseCommand, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseCommandHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Guid> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Exercise;

        // TODO: nahradit získáním ID přihlášeného uživatele
        var createdByUserId = Guid.NewGuid();

        // Mapování MediaUrls → ValueObjects
        var mediaItems = dto.MediaUrls
            .Select(url => new MediaItem(url, "video"))
            .ToList();

        var exercise = new Exercise(
            name: dto.Name,
            description: dto.Description ?? string.Empty,
            createdByUserId: createdByUserId,
            primaryMuscleGroup: dto.PrimaryMuscleGroup,
            equipment: dto.Equipment,
            media: mediaItems
        );

        await _exerciseRepository.AddAsync(exercise);

        return exercise.Id;
    }
}