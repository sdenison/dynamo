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
            var stepsFromOrigin = 0;
            if (coordinate.Q == 0 && coordinate.R == 0 && coordinate.S == 0)
            {
                return stepsFromOrigin;
            }
            while (coordinate.StepTowardZero())
            {
                stepsFromOrigin++;
            }
            return stepsFromOrigin;
        }

        public void ApplyDirections(string directions)
        {
            foreach (var direction in directions.Split(','))
            {
                var dxdy = new Coordinate(direction);
                Coordinate.Add(dxdy);

                var stepsFromOrigin = GetStepsFromOrigin(new Coordinate(Coordinate));
                if (FarthestAway < stepsFromOrigin)
                    FarthestAway = stepsFromOrigin;
            }
        }
    }
}
