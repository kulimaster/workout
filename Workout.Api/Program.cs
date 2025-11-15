using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Workout.Api.Filters;
using Workout.Api.Middlewares;
using Workout.Application;
using Workout.Infrastructure;
using Workout.Infrastructure.Configs;
using Workout.Infrastructure.Logging;
using Workout.Shared.Extensions;

namespace Workout.Api;

public class Program
{
    public static WebApplication BuildWebApplication(string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args ?? Array.Empty<string>());
        var env = builder.Environment;

        // Konfigurace appsettings
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        // Serilog
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console(new LogJsonFormatter())

            // .Enrich.FromLogContext()
            // .Enrich.WithSpan()
            .CreateLogger();

        builder.Host.UseSerilog();

        // Konfigurace DI
        builder.Services.AddConfigSingleton<PostgresConfig>(builder.Configuration, "Postgres");

        builder.Services.AddOpenApi();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddControllers(options =>
            {
                options.AddFilters();
            })
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false)
                );
            });

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        Log.ForContext<Program>().Information("Application started");

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseReDoc(options =>
            {
                options.DocumentTitle = "Workout API Docs";
                options.SpecUrl = "/openapi/v1.json";
            });
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<TraceIdHeaderMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<QueryLoggingMiddleware>();

        app.MapControllers();

        return app;
    }

    public static void Main(string[] args)
    {
        var app = BuildWebApplication(args);
        app.Run();
    }
}