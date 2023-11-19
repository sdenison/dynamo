namespace Dynamo.Business.Shared.AdventOfCode.Stars
{
    public class Velocity
    {
        public int X { get; }
        public int Y { get; }

        public Velocity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Velocity Parse(string pointString)
        {
            var openBracket = pointString.IndexOf("<");
            var closeBracket = pointString.IndexOf(">");
            var numbers = pointString.Substring(openBracket + 1, closeBracket - (openBracket + 1));
            var x = int.Parse(numbers.Split(',')[0]);
            var y = int.Parse(numbers.Split(',')[1]);
            return new Velocity(x, y);
        }
    }
}
