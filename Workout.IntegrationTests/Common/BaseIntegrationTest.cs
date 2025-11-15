namespace Workout.IntegrationTests.Common;

public abstract class BaseIntegrationTest
{
    protected readonly HttpClient client;

    protected BaseIntegrationTest()
    {
        var factory = new CustomWebApplicationFactory();
        client = factory.CreateClient(); // stejná DB během jednoho testu
    }
}