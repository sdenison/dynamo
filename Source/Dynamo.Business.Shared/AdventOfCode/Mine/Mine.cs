using Dynamo.Business.Shared.AdventOfCode.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Mine
    {
        public Point[,] Points { get; }

        public Mine(string[] mineLayout)
        {
            var xLength = mineLayout[0].Length;
            var yLength = mineLayout.Length;
            Points = new Point[xLength, yLength];
            for (var y = 0; y < yLength; y++)
                for (var x = 0; x < xLength; x++)
                    Points[x, y] = new Point(x, y, mineLayout[x][y]);


        }
    }
}
