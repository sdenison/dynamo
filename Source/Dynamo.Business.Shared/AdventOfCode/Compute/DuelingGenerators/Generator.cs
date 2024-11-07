namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class Generator : IGenerator
    {
        public string Id { get; protected set; }
        public long MultiplicationFactor { get; protected set; }
        protected long _lastNumberGeneratred = 0;
           
        public Generator(string id, long multiplicationFactor, long initialValue)
        {
            Id = id;
            MultiplicationFactor = multiplicationFactor;
            _lastNumberGeneratred = initialValue;
        }

        public long NextNumber()
        {
            var nextNumber = (_lastNumberGeneratred * MultiplicationFactor) % 2147483647;
            _lastNumberGeneratred = nextNumber;
            return nextNumber;
        }
    }
}
