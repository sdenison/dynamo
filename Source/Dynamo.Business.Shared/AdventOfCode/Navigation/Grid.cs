using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dynamo.Business.Shared.AdventOfCode.Fuel;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class Grid
    {
        public List<Coordinate> Coordinates { get; } = new List<Coordinate>();
        public GridPoint[,] GridPoints { get; }



        public Grid(string[] coordinates)
        {
            for (var i = 0; i < coordinates.Length; i++)
                Coordinates.Add(new Coordinate(coordinates[i], ((char)('A' + i)).ToString()));

            var maxXSize = Coordinates.Max(x => x.X) + 1;
            var maxYSize = Coordinates.Max(x => x.Y) + 1;
            var maxSize = maxXSize > maxYSize ? maxXSize : maxYSize;
            //Adding 1 since everything is zero based.
            GridPoints = new GridPoint[maxSize, maxSize];
            for (var x = 0; x < maxSize; x++)
                for (var y = 0; y < maxSize; y++)
                {
                    var minManhattan = Coordinates.Min(_ => _.GetManhattanDistance(x, y));
                    if (Coordinates.Count(_ => _.GetManhattanDistance(x, y) == minManhattan) == 1)
                    {
                        var ownedBy = Coordinates.Single(_ => _.GetManhattanDistance(x, y) == minManhattan);
                        GridPoints[x, y] = new GridPoint(ownedBy);
                    }
                    else
                    {
                        GridPoints[x, y] = new GridPoint();
                    }
                }
        }
    }
}
