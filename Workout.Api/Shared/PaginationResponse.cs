using System.Text.Json.Serialization;
using Workout.Api.Models;

namespace Workout.Api.Shared;

public class PaginationResponse<T> where T : IEnumerable<ExerciseModel>
{
    public T Data { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationMetadata? Pagination { get; set; }

}

public sealed class PaginationMetadata
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}