using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Cloth
{
    public class Sheet
    {
        public List<Coordinate> ApplyClaim(Claim claim)
        {
            var coordinaes = new List<Coordinate>();
            for (int x = claim.X; x < claim.X + claim.Width; x++)
                for (int y = claim.Y; y < claim.Y + claim.Height; y++)
                    coordinaes.Add(new Coordinate(x, y));
            return coordinaes;
        }

        public List<Coordinate> FindOverlap(List<Claim> claims)
        {
            var usedCoordinates = new HashSet<Coordinate>();
            var overlappingCoordinates = new HashSet<Coordinate>();
            foreach (var claim in claims)
            {
                var claimCoordinates = ApplyClaim(claim);
                foreach (var coordinate in claimCoordinates)
                {
                    if (usedCoordinates.Contains(coordinate))
                    {
                        if (!overlappingCoordinates.Contains(coordinate))
                            overlappingCoordinates.Add(coordinate);
                    }
                    else
                    {
                        usedCoordinates.Add(coordinate);
                    }
                }
            }
            return overlappingCoordinates.ToList();
        }
    }
}
