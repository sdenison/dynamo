namespace Dynamo.Business.Shared.AdventOfCode.Stars
{
    public class Point
    {
        public long X { get; set; }
        public long Y { get; set; }
        public Velocity Velocity { get; }

        public Point(long x, long y, Velocity velocity)
        {
            X = x;
            Y = y;
            Velocity = velocity;
        }

        public static Point Parse(string pointString)
        {
            var openBracket = pointString.IndexOf("<");
            var closeBracket = pointString.IndexOf(">");
            var numbers = pointString.Substring(openBracket + 1, closeBracket - (openBracket + 1));
            var x = long.Parse(numbers.Split(',')[0]);
            var y = long.Parse(numbers.Split(',')[1]);
            var velocityString = pointString.Substring(closeBracket + 1);
            return new Point(x, y, Velocity.Parse(velocityString));
        }

        public void Step()
        {
            X = X + Velocity.X;
            Y = Y + Velocity.Y;
        }
    }
}
