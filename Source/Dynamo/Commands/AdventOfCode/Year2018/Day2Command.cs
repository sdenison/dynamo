using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CommandLine;

namespace Dynamo.Commands.AdventOfCode.Year2018
{
    public class Day2Command : Command
    {
        public Day2Command() : base("day2", "Commands to solve day to puzzles")
        {
            var jobIdOption = CreateJobIdOption();
        }

        private Option<string> CreateJobIdOption()
        {
            var description = "Job ID to run";
            var option = new Option<string>(new string[2] { "--job-id", "-j" }, description);
            option.IsRequired = true;
            option.Arity = ArgumentArity.ExactlyOne;
            Add(option);
            return option;
        }
    }
}
