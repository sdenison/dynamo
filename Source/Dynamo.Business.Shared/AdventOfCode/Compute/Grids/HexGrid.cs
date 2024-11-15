using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class HexGrid
    {
        public Coordinate Coordinate { get; set; }
        public int FarthestAway { get; set; }

        public HexGrid()
        {
            Coordinate = new Coordinate();
        }

        public int GetStepsFromOrigin(Coordinate coordinate)
        {
            var q = Math.Abs(coordinate.Q);
            var r = Math.Abs(coordinate.R);
            var s = Math.Abs(coordinate.S);

            var orderedList = new List<int> { q, r, s };
            orderedList.Sort();
            return orderedList[2];
        }

        public void ApplyDirections(string directions)
        {
            foreach (var direction in directions.Split(','))
            {
                var delta = new Coordinate(direction);
                Coordinate.Add(delta);

                var stepsFromOrigin = GetStepsFromOrigin(new Coordinate(Coordinate));
                if (FarthestAway < stepsFromOrigin)
                    FarthestAway = stepsFromOrigin;
            }
        }
    }
}
