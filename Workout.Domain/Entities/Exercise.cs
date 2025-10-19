using System.ComponentModel.DataAnnotations.Schema;
using Workout.Domain.Enums;
using Workout.Domain.ValueObjects;
using Workout.Shared.Domain;

namespace Workout.Domain.Entities;

public class Exercise : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public MuscleGroup PrimaryMuscleGroup { get; private set; }
    public EquipmentType Equipment { get; private set; }
    
    [NotMapped]
    public List<MediaItem> Media { get; private set; }

    private Exercise() { } // EF Core

    public Exercise(
        string name,
        string description,
        Guid createdByUserId,
        MuscleGroup primaryMuscleGroup,
        EquipmentType equipment,
        IEnumerable<MediaItem>? media)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Exercise name cannot be empty.");

        Name = name;
        Description = description;
        CreatedByUserId = createdByUserId;
        PrimaryMuscleGroup = primaryMuscleGroup;
        Equipment = equipment;
        Media = media?.ToList() ?? new List<MediaItem>();
    }

    public void Update(string? name, string? description)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
    }
    
    public void AddMedia(MediaItem mediaItem) => Media.Add(mediaItem);
}
