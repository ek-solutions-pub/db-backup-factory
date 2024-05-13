namespace dbf_api.Configuration.Database.Postgres;

public class PostgresEnvironmentConfiguration : EnvironmentConfigurator, IPostgresConfiguration
{
    [EnvName("POSTGRES_HOST")]
    public string Host { get; set; } = null!;
    [EnvName("POSTGRES_PORT")]
    public int Port { get; set; } = 0;
    [EnvName("POSTGRES_DATABASE", false)]
    public string? Database { get; set; } = null!;
    [EnvName("POSTGRES_USER")]
    public string Username { get; set; } = null!;
    [EnvName("POSTGRES_PASSWORD")]
    public string Password { get; set; } = null!;
}
