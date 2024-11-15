using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class HexGrid
    {
        public Coordinate Coordinate { get; set; }
        public int FarthestAway { get; set; }

        public HexGrid()
        {
            Coordinate = new Coordinate();
        }

        public int GetStepsFromOrigin(Coordinate coordinate)
        {
            var stepsFromOrigin = 0;
            var q = Math.Abs(coordinate.Q);
            var r = Math.Abs(coordinate.R);
            var s = Math.Abs(coordinate.S);
            if (q <= r && q <= s)
            {
                stepsFromOrigin += q;
                if (r >= s)
                    stepsFromOrigin += r - q;
                else
                    stepsFromOrigin += s - q;
            }
            else if (r <= q && r <= s)
            {
                stepsFromOrigin += r;
                if (q >= s)
                    stepsFromOrigin += q - r;
                else
                    stepsFromOrigin += s - r;
            }
            else
            {
                stepsFromOrigin += s;
                if (q >= r)
                    stepsFromOrigin += q - s;
                else
                    stepsFromOrigin += r - s;
            }
            return stepsFromOrigin;
        }

        public void ApplyDirections(string directions)
        {
            foreach (var direction in directions.Split(','))
            {
                var dxdy = new Coordinate(direction);
                Coordinate.Add(dxdy);

                var stepsFromOrigin = GetStepsFromOrigin(new Coordinate(Coordinate));
                if (FarthestAway < stepsFromOrigin)
                    FarthestAway = stepsFromOrigin;
            }
        }
    }
}
