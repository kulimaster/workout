using MediatR;
using Moq;
using Workout.Application.Contracts.Persistence;
using Workout.Application.Exercises.Commands.CreateExercise;
using Workout.Application.Exercises.Commands.UpdateExercise;
using Workout.Domain.Enums;
using Workout.Domain.ValueObjects;
using Xunit;

namespace Workout.UnitTests.Exercise;

public class UpdateExerciseCommandHandlerTests
{
    private readonly Mock<IExerciseRepository> _repositoryMock;
    private readonly UpdateExerciseCommandHandler _handler;

    public UpdateExerciseCommandHandlerTests()
    {
        _repositoryMock = new Mock<IExerciseRepository>();
        _handler = new UpdateExerciseCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ExerciseExists_ShouldUpdateAndReturnUnit()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();

        var existingExercise = new Domain.Entities.Exercise(
            name: "Push-up",
            description: "desc",
            createdByUserId: Guid.NewGuid(),
            primaryMuscleGroup: MuscleGroup.Chest,
            equipment: EquipmentType.None,
            media: new List<MediaItem>()
        );

        _repositoryMock
            .Setup(r => r.GetByIdAsync(exerciseId))
            .ReturnsAsync(existingExercise);

        var command = new UpdateExerciseCommand(
            exerciseId,
            new ExerciseDto(
                Name: "Pull-up",
                Description: "new desc",
                PrimaryMuscleGroup: MuscleGroup.Back,
                Equipment: EquipmentType.Dumbbell,
                MediaUrls: new List<string> { "url1", "url2" }
            )
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);

        _repositoryMock.Verify(
            r =>
                r.UpdateAsync(
                    It.Is<Domain.Entities.Exercise>(e =>
                        e.Name == "Pull-up" &&
                        e.Description == "new desc" &&
                        e.PrimaryMuscleGroup == MuscleGroup.Back &&
                        e.Equipment == EquipmentType.Dumbbell

                        // todo && e.Media.Count == 2
                    )
                ),
            Times.Once
        );
    }
}