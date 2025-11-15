using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Workout.Api.Models;
using Workout.Domain.Enums;
using Workout.IntegrationTests.Common;
using Xunit;

namespace Workout.IntegrationTests;

public class ExercisesApiTests : BaseIntegrationTest
{
    [Fact]
    public async Task CreateExercise_Then_Get_ReturnsCreatedExercise()
    {
        // ARRANGE
        var request = new
        {
            name = "Bench Press",
            description = "Chest exercise",
            primaryMuscleGroup = "back",
            equipment = "machine",
            mediaUrls = new List<string>()
        };

        // ACT POST
        var postResponse = await client.PostAsJsonAsync("/api/exercise", request);

        // ASSERT
        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        var id = await postResponse.Content.ReadFromJsonAsync<string>();
        var guid = Guid.Parse(id!);
        Assert.NotNull(id);
        Assert.NotEqual(Guid.Empty, guid);

        // ACT GET
        var getResponse = await client.GetAsync($"/api/exercise");
        getResponse.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
        var loaded = await getResponse.Content.ReadFromJsonAsync<GetExercisesResponse>(options);

        // ASSERT
        Assert.NotNull(loaded);
        Assert.Equal("Bench Press", loaded!.Data[0].Name);
        Assert.Equal("Chest exercise", loaded.Data[0].Description);
        Assert.Equal(MuscleGroup.Back, loaded.Data[0].PrimaryMuscleGroup);
        Assert.Equal(EquipmentType.Machine, loaded.Data[0].Equipment);
    }
}