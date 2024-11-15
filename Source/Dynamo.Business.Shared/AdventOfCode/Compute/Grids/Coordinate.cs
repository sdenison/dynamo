using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate()
        {
            X = 0;
            Y = 0;
        }

        public void Add(Coordinate coord)
        {
            X += coord.X;
            Y += coord.Y;
        }

        public Coordinate(string direction)
        {
            switch (direction)
            {
                case "ne":
                    X = 1; Y = 1;
                    break;
                case "n":
                    X = 0; Y = 1;
                    break;
                case "nw":
                    X = -1; Y = 1;
                    break;
                case "sw":
                    X = -1; Y = -1;
                    break;
                case "s":
                    X = 0; Y = -1;
                    break;
                case "se":
                    X = 1; Y = -1;
                    break;
                default:
                    throw new ArgumentException($"Unknown direction {direction}");
            };
        }
    }
}
