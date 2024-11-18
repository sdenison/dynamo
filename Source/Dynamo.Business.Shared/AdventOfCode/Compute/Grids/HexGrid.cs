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

        public int GetStepsFromOrigin()
        {
            var q = Math.Abs(Coordinate.Q);
            var r = Math.Abs(Coordinate.R);
            var s = Math.Abs(Coordinate.S);

            var orderedList = new List<int> { q, r, s };
            orderedList.Sort();
            //return orderedList[0] + (orderedList[2] - orderedList[0]);
            return orderedList[2];
        }

        public void ApplyDirections(string directions)
        {
            foreach (var direction in directions.Split(','))
            {
                var delta = new Coordinate(direction);
                Coordinate.Add(delta);

                var stepsFromOrigin = GetStepsFromOrigin();
                if (FarthestAway < stepsFromOrigin)
                    FarthestAway = stepsFromOrigin;
            }
        }
    }
}
