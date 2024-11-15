namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class HexGrid
    {
        public Coordinate Coordinate { get; set; }

        public HexGrid()
        {
            Coordinate = new Coordinate();
        }

        public int GetStepsFromOrigin()
        {
            var stepsFromOrigin = 0;
            if (Coordinate.Q == 0 && Coordinate.R == 0 && Coordinate.S == 0)
            {
                return stepsFromOrigin;
            }
            while (Coordinate.StepTowardZero())
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
            }
        }
    }
}
