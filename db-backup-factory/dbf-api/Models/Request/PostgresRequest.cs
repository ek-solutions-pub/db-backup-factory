using System.Text.Json.Serialization;
using dbf_api.Configuration.Database.Postgres;

namespace dbf_api.Models.Request;

public class PostgresRequest
{
    [JsonPropertyName("auth_type")]
    public AuthType AuthenticationType { get; set; }

    [JsonPropertyName("auth")]
    public PostgresConfiguration? Authentication { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuthType
    {
        [JsonPropertyName("environment_variables")]
        EnvironmentVariables,

        [JsonPropertyName("passed_in")]
        PassedIn,
    }
}
