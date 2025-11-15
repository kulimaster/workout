using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Serilog;
using Workout.Api;

namespace Workout.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            context.HostingEnvironment.EnvironmentName = "Test";
        });

        builder.ConfigureServices(services =>
        {
            // Zde vypneš Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Fatal() // loguje jen fatální chyby = prakticky nic
                .CreateLogger();
        });
    }
}
