namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Marble
    {
        public int Value { get; }
        public Marble NextMarble { get; set; }
        public Marble PreviousMarble { get; set; }

        public Marble(int value)
        {
            Value = value;
        }

        public Marble(int value, Marble previousMarble, Marble nextMarble)
        {
            Value = value;
            PreviousMarble = previousMarble;
            NextMarble = nextMarble;
        }
    }
}
