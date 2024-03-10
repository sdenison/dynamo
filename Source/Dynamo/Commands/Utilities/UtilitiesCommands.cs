using System.CommandLine;

namespace Dynamo.Commands.Utilities
{
    internal class UtilitiesCommands : Command
    {
        public UtilitiesCommands() : base("utilities", "Utilities command")
        {
            Add(new DiagnosticsCommand());
            Add(new RunBackgroundJobCommand());
            Add(new SendEmailCommand());
        }
    }
}
