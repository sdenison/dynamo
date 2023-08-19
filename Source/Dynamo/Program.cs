using System.CommandLine;

namespace Dynamo
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootLevelCommands = new RootLevelCommands();
            var returnValue = await rootLevelCommands.InvokeAsync(args);
            return returnValue;
        }

		//This is exposed so the Lambda functions have access to anything the console appliation does.
		public static async Task InvokeAsync(string[] args)
		{
			try
			{
				//The next two lines are all that's needed for the full cli.
				var newRoot = new RootLevelCommands();
				await newRoot.InvokeAsync(args);
			}
			catch (Exception ex)
			{
				var x = ex.InnerException;
			}
		}
    }
}