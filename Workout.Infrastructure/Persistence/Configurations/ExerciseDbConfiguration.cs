using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entities;
using Workout.Domain.Enums;

namespace Workout.Infrastructure.Persistence.Configurations;

public class ExerciseDbConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.PrimaryMuscleGroup).HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.Equipment).HasConversion<string>().HasMaxLength(50);
        
        builder.Ignore(e => e.CreatedByUserId);
        
        
        
        
        /*.HasConversion(
            v => JsonHelper.Serialize(v),
            v => JsonHelper.Deserialize<EquipmentType>(v) ?? new List<EquipmentType>());*/

        // Media jako samostatná tabulka
        /*builder.HasMany(e => e.Media)
            .WithOne()
            .HasForeignKey("ExerciseId")
            .OnDelete(DeleteBehavior.Cascade);*/
    }
    
    public static class JsonHelper
    {
        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}