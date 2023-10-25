namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Circle
    {
        public Marble CurrentMarble { get; set; }

        public Circle()
        {
            //Sets up the circular linked list by hooking up marble 0 to marble 1 and vice versa
            var marble0 = new Marble(0);
            var marble1 = new Marble(1);
            marble0.NextMarble = marble1;
            marble0.PreviousMarble = marble1;
            marble1.NextMarble = marble0;
            marble1.PreviousMarble = marble0;
            CurrentMarble = marble1;
        }

        public void AddMarble(int marbleValue)
        {
            var previous = CurrentMarble.NextMarble;
            var next = CurrentMarble.NextMarble.NextMarble;
            var newMarble = new Marble(marbleValue, previous, next);
            previous.NextMarble = newMarble;
            next.PreviousMarble = newMarble;
            CurrentMarble = newMarble;
        }

        public Marble CollectMarble()
        {
            var marble = CurrentMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble;
            marble.PreviousMarble.NextMarble = marble.NextMarble;
            marble.NextMarble.PreviousMarble = marble.PreviousMarble;
            CurrentMarble = CurrentMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble.PreviousMarble;
            return marble;
        }
    }
}
