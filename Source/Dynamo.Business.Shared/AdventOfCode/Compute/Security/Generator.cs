namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Generator
    {
        public int Depth { get; private set; }
        public int CurrentDepth { get; private set; }
        public bool GoingDown { get; private set; }

        public Generator(int depth)
        {
            Depth = depth;
            CurrentDepth = 0;
            GoingDown = false;
        }

        public void ElapsePicoSecond()
        {
            CurrentDepth++;
            if (GoingDown && CurrentDepth < Depth)
            {
            }
        }
    }
}
