using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Commands.AdventOfCode.Year2018
{
    public class Year2018Commands : Command
    {
        public Year2018Commands() : base("year2018", "Puzzle solvers for AOC 2018")
        {
            Add(new Day2Command());
        }
    }
}
