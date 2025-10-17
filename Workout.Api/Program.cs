using Workout.Api.Filters;
using Workout.Api.Middlewares;
using Workout.Application;
using Workout.Infrastructure;
using Workout.Infrastructure.Configs;
using Workout.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddConfigSingleton<PostgresConfig>(builder.Configuration, "Postgres");

builder.Services.AddOpenApi();

builder.Services.AddControllers(options =>
{
    options.AddFilters();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

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
app.UseMiddleware<ExceptionMiddleware>();



app.MapControllers();


app.Run();

