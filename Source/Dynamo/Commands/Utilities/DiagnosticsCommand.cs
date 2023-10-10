using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.CommandLine;
using System.Reflection;

namespace Dynamo.Commands.Utilities
{
    internal class DiagnosticsCommand : Command
    {
        public DiagnosticsCommand() : base("diagnostics", "Displays diagnostics information.")
        {
            var writeToLogOption = CreateWriteToLogOption();
            this.SetHandler((writeToLog) => { DisplayDiagnosticInformation(writeToLog); }, writeToLogOption);
        }

        public void DisplayDiagnosticInformation(bool writeToLog)
        {
            var assemblyName = Assembly.GetEntryAssembly().FullName;
            WriteLine($"Assembly Name: {assemblyName}");
            WriteLine($"Framework Version: {Environment.Version}");
            WriteLine($"OS Version: {Environment.OSVersion}");
            WriteLine($"Machine Name: {Environment.MachineName}");
            WriteLine($"Build Number: {GetBuildNumber()}");
            WriteLine($"Current User: {Environment.UserName}");
            WriteLine($"DateTime.Now: {DateTime.Now}");
            WriteLine($"UTC DateTime: {DateTime.UtcNow}");
            foreach (var drive in DriveInfo.GetDrives())
            {
                WriteLine(
                    $"Drive Name: {drive.Name}, Size: {drive.TotalSize}, Free Space: {drive.AvailableFreeSpace}, Drive Format: {drive.DriveFormat}");
            }

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var totalMemory = (double)gcMemoryInfo.TotalAvailableMemoryBytes / 1048576.0;
            WriteLine($"Total memory on machine in megabytes: {totalMemory}");
            var memoryUsedByProcess = (double)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1048576.0;
            WriteLine($"Memory used by this process in megabytes: {memoryUsedByProcess}");
            WriteLine($"writeToLog = {writeToLog}");
            WriteLine($"This is an updated image 2");
        }

        private Option<bool> CreateWriteToLogOption()
        {
            //When set to true the dignostics will write to log if possible.
            var description = "Write to log";
            var writeToLogOption = new Option<bool>(new string[2] { "--write-to-log", "-w" }, description);
            writeToLogOption.IsRequired = false;
            writeToLogOption.Arity = ArgumentArity.ZeroOrOne;
            Add(writeToLogOption);
            return writeToLogOption;
        }

        private void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        private string GetBuildNumber()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var release = configuration["Release"];
            var buildNumber = $"{release}.{configuration["BuildNumber"]}";
            return buildNumber;
        }
    }
}
