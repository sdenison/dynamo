using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class GridPoint
    {
        public Coordinate OwnedBy { get; }

        public GridPoint()
        {
        }

        public GridPoint(Coordinate ownedBy)
        {
            OwnedBy = ownedBy;
        }
    }
}
