using System.CommandLine;

namespace Dynamo
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootLevelCommands = new RootLevelCommands();
            var returnValue = await rootLevelCommands.InvokeAsync(args);
            return returnValue;
        }
    }
}