using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Workout.Domain.Enums;

namespace Workout.Api.Models;

public class ExerciseModel
{
    [JsonPropertyName("name")]
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("primaryMuscleGroup")]
    [Required(ErrorMessage = "PrimaryMuscleGroup is required.")]
    public MuscleGroup PrimaryMuscleGroup { get; set; }

    [JsonPropertyName("equipment")]
    public EquipmentType Equipment { get; set; } = new();

    [JsonPropertyName("mediaUrls")]
    public List<string> MediaUrls { get; set; } = new();
}