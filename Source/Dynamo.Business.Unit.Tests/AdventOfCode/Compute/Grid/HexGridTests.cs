using Dynamo.Business.Shared.AdventOfCode.Compute.Grids;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Grid
{
    [TestFixture]
    public class HexGridTests
    {
        [Test]
        public void Can_create_HexGrid()
        {
            var hexGrid = new HexGrid();
            Assert.That(hexGrid, Is.Not.Null);
        }

        [Test]
        public void Can_apply_directions_to_new_HexGrid()
        {
            var hexGrid = new HexGrid();
            hexGrid.ApplyDirections("ne,ne,ne");
            Assert.That(hexGrid.Coordinate.X, Is.EqualTo(3));
            Assert.That(hexGrid.Coordinate.Y, Is.EqualTo(3));
            Assert.That(hexGrid.GetStepsFromOrigin(), Is.EqualTo(3));
        }
    }
}
