using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class Grid
    {
        public List<Coordinate> Coordinates { get; }
        public GridPoint[,] GridPoints { get; }
        private readonly int _maxXSize;
        private readonly int _maxYSize;

        public static List<Coordinate> ParseCoordinates(string[] coordinates)
        {
            var result = new List<Coordinate>();
            for (var i = 0; i < coordinates.Length; i++)
                result.Add(new Coordinate(coordinates[i], ((char)('A' + i)).ToString()));
            return result;
        }

        public Grid(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
            _maxXSize = Coordinates.Max(x => x.X) + 1;
            _maxYSize = Coordinates.Max(x => x.Y) + 1;
            //Adding 1 since everything is zero based.
            GridPoints = new GridPoint[_maxXSize, _maxYSize];
            for (var x = 0; x < _maxXSize; x++)
                for (var y = 0; y < _maxYSize; y++)
                {
                    var minManhattan = Coordinates.Min(_ => _.GetManhattanDistance(x, y));
                    if (Coordinates.Count(_ => _.GetManhattanDistance(x, y) == minManhattan) == 1)
                    {
                        var ownedBy = Coordinates.Single(_ => _.GetManhattanDistance(x, y) == minManhattan);
                        GridPoints[x, y] = new GridPoint(ownedBy);
                        if (x == _maxXSize - 1 || x == 0 || y == _maxYSize - 1 || y == 0)
                            ownedBy.IsInfinite = true;
                    }
                    else
                    {
                        GridPoints[x, y] = new GridPoint();
                    }
                }
        }

        public int GetArea(Coordinate coordinate)
        {
            var area = 0;
            for (var x = 0; x < _maxXSize; x++)
                for (var y = 0; y < _maxYSize; y++)
                    if (GridPoints[x, y].OwnedBy != null && GridPoints[x, y].OwnedBy == coordinate)
                        area++;
            return area;
        }

        public int GetLargesAreaNotInfinite()
        {

            var largestArea = 0;
            foreach (var coordinate in Coordinates.Where(x => x.IsInfinite == false))
            {
                var area = GetArea(coordinate);
                if (area > largestArea)
                    largestArea = area;
            }
            return largestArea;
        }
    }
}
