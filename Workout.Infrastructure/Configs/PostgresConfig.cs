namespace Workout.Infrastructure.Configs;

public class PostgresConfig
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Schema { get; set; } = string.Empty;
    
    public string BuildConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Schema};Username={User};Password={Password}";
    }
}