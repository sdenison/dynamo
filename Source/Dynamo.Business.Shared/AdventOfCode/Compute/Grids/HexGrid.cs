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
            return 0;
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
