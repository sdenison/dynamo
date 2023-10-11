namespace Dynamo.Business.Shared.AdventOfCode.Cloth
{
    public class Claim
    {
        public int Id { get; set; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Claim(int id, int x, int y, int width, int height)
        {
            Id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Claim Parse(string s)
        {
            var id = int.Parse(s.Split(' ')[0].Replace("#", ""));
            var x = int.Parse(s.Split(' ')[2].Split(',')[0]);
            var y = int.Parse(s.Split(' ')[2].Split(',')[1].Replace(":", ""));
            var width = int.Parse(s.Split(' ')[3].Split("x")[0]);
            var height = int.Parse(s.Split(' ')[3].Split("x")[1]);
            return new Claim(id, x, y, width, height);
        }
    }
}
