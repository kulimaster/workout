using Workout.Domain.Enums;
using Workout.Domain.ValueObjects;
using Workout.Shared.Domain;

namespace Workout.Domain.Entities;

public class Exercise : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public Guid CreatedByUserId { get; private set; } // kdo cvik přidal
    public MuscleGroup MuscleGroups { get; private set; } = new();
    public List<EquipmentType> Equipment { get; private set; } = new();
    public List<MediaItem> Media { get; private set; } = new();

    private Exercise() { } // EF Core

    public Exercise(string name, string description, string category,
        Guid createdByUserId,
        MuscleGroup muscleGroups,
        IEnumerable<EquipmentType> equipment,
        IEnumerable<MediaItem> media)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Exercise name cannot be empty.");

        Name = name;
        Description = description;
        Category = category;
        CreatedByUserId = createdByUserId;
        MuscleGroups = muscleGroups;
        Equipment.AddRange(equipment);
        Media.AddRange(media);
    }

    // Metody pro editaci cviku (pokud uživatel smí upravovat)
    public void Update(string? name, string? description, IEnumerable<MuscleGroup>? muscleGroups)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(description)) Description = description;
        
        
        
    }
}