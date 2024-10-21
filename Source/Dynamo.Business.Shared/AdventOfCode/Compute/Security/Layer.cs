namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Layer
    {
        public int LayerId { get; private set; }
        public int Range { get; private set; }
        public int CurrentDepth { get; private set; }
        private bool _goingDown = true;

        public Layer(int layerId, int range)
        {
            LayerId = layerId;
            Range = range;
            CurrentDepth = 1;
        }

        public void AdvanceOnePicosecond()
        {
            if (CurrentDepth == Range && _goingDown)
            {
                _goingDown = false;
            }
            if (CurrentDepth == 1 && !_goingDown)
            {
                _goingDown = true;
            }
            if (_goingDown)
            {
                CurrentDepth++;
            }
            else
            {
                CurrentDepth--;
            }
        }

    }
}
