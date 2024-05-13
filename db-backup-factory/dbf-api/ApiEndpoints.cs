using dbf_api.Configuration;
using dbf_api.Configuration.Database.Postgres;
using dbf_api.Configuration.Encryption;
using dbf_api.Driver.Database;
using dbf_api.Driver.Shell;
using dbf_api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace dbf_api;

public static class ApiEndpoints
{
    public static void MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/pg_dump", async ([FromBody] PostgresRequest request, HttpContext context) =>
        {
            var creationDate = DateTime.UtcNow;

            context.Response.Headers.Append("Creation-Date", creationDate.ToString("O"));
            context.Response.Headers.Append("Content-Disposition", $"attachment; filename=\"backup-{creationDate:yyyy-MM-dd_HH-mm-ss}.sql.enc\"");
            //TODO: Read API Version from configuration or appropriate source
            context.Response.Headers.Append("Api-Version", "1.0");

            IPostgresConfiguration postgresConfiguration = request.AuthenticationType switch {
                PostgresRequest.AuthType.EnvironmentVariables => new PostgresEnvironmentConfiguration(),
                PostgresRequest.AuthType.PassedIn => request.Authentication ?? throw new InvalidOperationException("Authentication details must not be null"),
                _ => throw new ArgumentOutOfRangeException(nameof(request.AuthenticationType), request.AuthenticationType, "Unsupported authentication type")
            };
            var shellDriver = new ShellDriver();

            var driver = new PostgresDriver(postgresConfiguration, shellDriver);
            var backupStream = await driver.GetBackupStream();
            var encryptionConfiguration = EncryptionConfiguration.FromEnvironment();
            var encryptedStream = StreamEncryptor.EncryptStream(backupStream.BaseStream, encryptionConfiguration.Key, encryptionConfiguration.IV);

            return Results.File(encryptedStream, "application/octet-stream");
        });
    }
}
