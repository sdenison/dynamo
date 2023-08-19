using Dynamo.Commands.Utilities;
using System.CommandLine;

namespace Dynamo
{
    internal class RootLevelCommands : RootCommand
    {
        public RootLevelCommands() 
        {
            Add(new UtilitiesCommands());
        }
    }
}
