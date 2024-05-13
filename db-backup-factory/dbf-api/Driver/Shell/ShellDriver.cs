using System.Diagnostics;

namespace dbf_api.Driver.Shell;

public class ShellDriver
{
    public ShellResult ExecuteShellCommand(string command, Dictionary<string,string>? environmentVariables = null)
    {
        var path = Environment.GetEnvironmentVariable("PATH");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command.Replace("\"", "\\\"")}\"" ,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.StartInfo.Environment["PATH"] = path;
        
        if (environmentVariables != null)
        {
            foreach (var (key, value) in environmentVariables)
            {
                process.StartInfo.EnvironmentVariables[key] = value;
            }
        }

        try
        {
            process.Start();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new ShellResult(process);
    }
}
