namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Layer
    {
        public int LayerId { get; private set; }
        public int Range { get; private set; }
        public int SecurityScanDepth { get; private set; }
        private bool _goingDown = true;

        public Layer(int layerId, int range)
        {
            LayerId = layerId;
            Range = range;
            SecurityScanDepth = 1;
        }

        public Layer(string layerData)
        {
            var parts = layerData.Split(' ');
            int layerId = int.Parse(parts[0].Replace(":", ""));
            int range = int.Parse(parts[1]);

            this.LayerId = layerId;
            this.Range = range;
            SecurityScanDepth = 1;
        }

        public void AdvanceOnePicosecond()
        {
            if (SecurityScanDepth == Range && _goingDown && Range > 0)
            {
                _goingDown = false;
            }
            if (SecurityScanDepth == 1 && !_goingDown && Range > 0)
            {
                _goingDown = true;
            }
            if (_goingDown)
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
