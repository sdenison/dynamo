using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class Judge
    {
        private readonly List<IGenerator> _generators;

        public Judge(List<IGenerator> generators)
        {
            _generators = generators;
        }

        public int CountMatches(int pairsToProcess)
        {
            var matches = 0;
            for(var i = 0; i < pairsToProcess; i++) 
            {
                var generator1Value = _generators[0].NextNumber();
                var generator2Value = _generators[1].NextNumber();
                if ((generator1Value % 65536) ==  (generator2Value % 65536))
                {
                    matches++;
                }
            }
            return matches;
        }
    }
}
