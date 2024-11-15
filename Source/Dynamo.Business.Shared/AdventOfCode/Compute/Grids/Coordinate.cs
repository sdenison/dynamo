using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class Coordinate
    {
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }

        public Coordinate()
        {
            Q = 0;
            R = 0;
            S = 0;
        }

        public Coordinate(Coordinate coordinate)
        {
            Q = coordinate.Q;
            R = coordinate.R;
            S = coordinate.S;
        }

        public void Add(Coordinate coord)
        {
            Q += coord.Q;
            R += coord.R;
            S += coord.S;
        }

        public Coordinate(string direction)
        {
            switch (direction)
            {
                case "ne":
                    Q = 1; R = -1; S = 0;
                    break;
                case "n":
                    Q = 0; R = -1; S = 1;
                    break;
                case "nw":
                    Q = -1; R = 0; S = 1;
                    break;
                case "sw":
                    Q = -1; R = 1; S = 0;
                    break;
                case "s":
                    Q = 0; R = 1; S = -1;
                    break;
                case "se":
                    Q = 1; R = 0; S = -1;
                    break;
                default:
                    throw new ArgumentException($"Unknown direction {direction}");
            };
        }
    }
}
