using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);

        public Point(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
