namespace Dynamo.Business.Shared.AdventOfCode.Cloth
{
    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
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
