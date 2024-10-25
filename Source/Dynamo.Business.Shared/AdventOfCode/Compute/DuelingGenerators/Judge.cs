using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class Judge
    {
        private readonly List<Generator> _generators;

        public Judge(List<Generator> generators)
        {
            _generators = generators;
        }
    }
}
