namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Layer
    {
        public int Range { get; private set; }
        public int SecurityScanDepth { get; private set; }
        private bool _goingDown = true;

        public Layer(int range)
        {
            Range = range;
            if (Range == 0)
                SecurityScanDepth = 0;
            else
                SecurityScanDepth = 1;
        }

        public Layer(string layerData)
        {
            var parts = layerData.Split(' ');
            int range = int.Parse(parts[1]);
            this.Range = range;
            if (Range == 0)
                SecurityScanDepth = 0;
            else
                SecurityScanDepth = 1;
        }
        public void AdvanceOnePicosecond()
        {
            if (Range == 0)
                return;
            if (SecurityScanDepth == Range && _goingDown)
            {
                _goingDown = false;
            }
            if (SecurityScanDepth == 1 && !_goingDown)
            {
                _goingDown = true;
            }
            if (_goingDown && Range > 0)
            {
                SecurityScanDepth++;
            }
            else
            {
                SecurityScanDepth--;
            }
        }

        public bool IsScannerActive()
        {
            if (Range > 0 && SecurityScanDepth == 1)
            {
                return true;
            }
            return false;
        }
    }
}
