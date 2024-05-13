using dbf_api.Configuration;
using dbf_api.Driver.Shell;

namespace dbf_api.Driver.Database;

public class PostgresDriver(IPostgresConfiguration configuration, ShellDriver shellDriver) : IDatabaseDriver
{
    public Task<StreamReader> GetBackupStream()
    {
        var baseArguments = $"-h {configuration.Host} -p {configuration.Port} -U {configuration.Username}";
        var command = string.IsNullOrEmpty(configuration.Database) 
                    ? $"pg_dumpall {baseArguments}" 
                    : $"pg_dump {baseArguments} -d {configuration.Database}";

        var result = shellDriver.ExecuteShellCommand(command, new() {
            {"PGPASSWORD", configuration.Password}
        });
        return Task.FromResult(result.Output);
    }
}
