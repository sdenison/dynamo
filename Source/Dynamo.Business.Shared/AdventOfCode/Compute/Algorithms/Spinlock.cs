namespace Dynamo.Business.Shared.AdventOfCode.Compute.Algorithms
{
    public class Spinlock
    {
        public int Value { get; set; }
        public Spinlock Previous { get; set; }
        public Spinlock Next { get; set; }

        public Spinlock()
        {
            Value = 0;
            Previous = this;
            Next = this;
        }

        public Spinlock(int value, Spinlock previous, Spinlock next)
        {
            Previous = previous;
            Next = next;
            Value = value;
        }

        public Spinlock Add(int steps, int value)
        {
            var current = MoveForward(steps);
            var newSpinlock = new Spinlock(value, current, current.Next);
            current.Next = newSpinlock;
            return newSpinlock;
        }

        public Spinlock MoveForward(int steps)
        {
            if (steps == 0) return this;
            steps--;
            return Next.MoveForward(steps);
        }
    }
}
