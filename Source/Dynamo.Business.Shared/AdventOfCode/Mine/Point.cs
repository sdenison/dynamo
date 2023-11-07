namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public char PointChar { get; }

        public Point(int x, int y, char pointChar)
        {
            X = x;
            Y = y;
            PointChar = pointChar;
        }
    }
}
