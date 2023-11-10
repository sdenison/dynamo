using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Stars
{
    public class Sky
    {
        public List<Point> Points { get; }

        public long MinX { get; }
        public long MaxX { get; }
        public long MinY { get; }
        public long MaxY { get; }

        public Sky(string[] pointStrings)
        {
            Points = new List<Point>();
            foreach (var pointString in pointStrings)
                Points.Add(Point.Parse(pointString));
            MinX = Points.Min(x => x.X);
            MaxX = Points.Max(x => x.X);
            MinY = Points.Min(x => x.Y);
            MaxY = Points.Max(x => x.Y);
        }

        public void Step()
        {
            Points.ForEach(x => x.Step());
        }

        public List<string> DisplayCondensed()
        {
            var minX = Points.Min(x => x.X);
            var maxX = Points.Max(x => x.X);
            var minY = Points.Min(x => x.Y);
            var maxY = Points.Max(x => x.Y);
            var displayStrings = new List<string>();
            for (var y = minY; y <= maxY; y++)
            {
                var line = new StringBuilder();
                for (var x = minX; x <= maxX; x++)
                {
                    if (Points.Any(point => point.X == x && point.Y == y))
                        line.Append("#");
                    else
                        line.Append(".");
                }
                displayStrings.Add(line.ToString());
            }
            return displayStrings;
        }

        public List<string> Display()
        {
            var displayStrings = new List<string>();
            for (var y = MinY; y <= MaxY; y++)
            {
                var line = new StringBuilder();
                for (var x = MinX; x <= MaxX; x++)
                {
                    if (Points.Any(point => point.X == x && point.Y == y))
                        line.Append("#");
                    else
                        line.Append(".");
                }
                displayStrings.Add(line.ToString());
            }
            return displayStrings;
        }
    }
}
