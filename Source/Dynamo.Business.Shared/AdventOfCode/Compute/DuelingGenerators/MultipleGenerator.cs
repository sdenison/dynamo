namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class MultipleGenerator : Generator, IGenerator
    {
        private int _multiple;

        public MultipleGenerator(string id, long multiplicationFactor, long initialValue, int multiple) :
            base(id, multiplicationFactor, initialValue)
        {
            _multiple = multiple;
        }

        public new long NextNumber()
        {
            while(true)
            {
                var nextNumber = base.NextNumber();
                if (nextNumber % _multiple == 0)
                    return nextNumber;
            }
        }
    }
}
