
using System.Text.Json.Serialization;

namespace dbf_api.Configuration.Database.Postgres;

public class PostgresConfiguration: IPostgresConfiguration
{
    [JsonPropertyName("host")]
    public required string Host { get; init; }
    [JsonPropertyName("port")]
    public required int Port { get; init; }
    [JsonPropertyName("database")]
    public required string Database { get; init; }
    [JsonPropertyName("username")]
    public required string Username { get; init; }
    [JsonPropertyName("password")]
    public required string Password { get; init; }
}
