using System.Diagnostics;

public static class CommandLineHelper
{
    public static int RunCommandLine(string command)
    {
        var processInfo = new ProcessStartInfo();
        processInfo.FileName = "CMD.EXE";
        processInfo.Arguments = command;
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;

        var process = Process.Start(processInfo);

        process.WaitForExit();
        var exitCode = process.ExitCode;
        process.Close();

        return exitCode;
    }

    public static int RunPowershellLine(string command)
    {
        var processInfo = new ProcessStartInfo();
        processInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
        processInfo.Arguments = command;
        processInfo.CreateNoWindow = false;
        processInfo.UseShellExecute = false;

        var process = Process.Start(processInfo);

        process.WaitForExit();
        var exitCode = process.ExitCode;
        process.Close();

        return exitCode;
    }
}
