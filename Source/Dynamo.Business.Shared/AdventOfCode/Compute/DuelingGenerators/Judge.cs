namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class Judge
    {
        private readonly IGenerator _generatorA;
        private readonly IGenerator _generatorB;

        public Judge(IGenerator generatorA, IGenerator generatorB)
        {
            _generatorA = generatorA;
            _generatorB = generatorB;
        }

        public int CountMatches(int pairsToProcess)
        {
            var matches = 0;
            for(var i = 0; i < pairsToProcess; i++) 
                if ((_generatorA.NextNumber() % 65536) == (_generatorB.NextNumber() % 65536))
                    matches++;
            return matches;
        }
    }
}
