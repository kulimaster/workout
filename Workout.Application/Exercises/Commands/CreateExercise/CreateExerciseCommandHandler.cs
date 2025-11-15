using MediatR;
using Workout.Application.Contracts.Persistence;
using Workout.Domain.Entities;
using Workout.Domain.ValueObjects;

namespace Workout.Application.Exercises.Commands.CreateExercise;

internal sealed class CreateExerciseCommandHandler(IExerciseRepository exerciseRepository)
    : IRequestHandler<CreateExerciseCommand, Guid>
{
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

        var result = await exerciseRepository.AddAsync(exercise);

        return result.Id;
    }
}