using System;
using System.Runtime.InteropServices.ComTypes;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }
        public string Name { get; }
        public int GetManhattanDistance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }

        public Coordinate(string coordinates, string name) : this(int.Parse(coordinates.Split(',')[0]), int.Parse(coordinates.Split(',')[1]), name)
        {
        }

        public Coordinate(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Coordinate other = (Coordinate)obj;
            return X == other.X && Y == other.Y;
        }
    }
}
