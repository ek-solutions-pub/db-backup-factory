using System.Diagnostics;

namespace dbf_api;

public class ShellResult
{
    public int? ExitCode { get; set; } = null;
    public StreamReader Output  => Process.StandardOutput;
    public StreamReader Error => Process.StandardError;
    public Process Process { get; init; }
    public event Func<int, Task>? OnProcessExited;
    public Task WaitForExit { get; set; }        
    public ShellResult(Process process)
    {
        Process = process;
        WaitForExit = Process.WaitForExitAsync().ContinueWith((task, o) => {
            ExitCode = Process.ExitCode;
            OnProcessExited?.Invoke(ExitCode.Value);
        }, null);
    }
  
    
}
