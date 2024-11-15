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
            Assert.That(hexGrid.Coordinate.Q, Is.EqualTo(3));
            Assert.That(hexGrid.Coordinate.R, Is.EqualTo(-3));
            Assert.That(hexGrid.Coordinate.S, Is.EqualTo(0));
            Assert.That(hexGrid.GetStepsFromOrigin(hexGrid.Coordinate), Is.EqualTo(3));
        }

        [Test]
        public void Can_apply_directions_to_new_HexGrid_example_2()
        {
            var hexGrid = new HexGrid();
            hexGrid.ApplyDirections("ne,ne,sw,sw");
            Assert.That(hexGrid.Coordinate.Q, Is.EqualTo(0));
            Assert.That(hexGrid.Coordinate.R, Is.EqualTo(0));
            Assert.That(hexGrid.Coordinate.S, Is.EqualTo(0));
            Assert.That(hexGrid.GetStepsFromOrigin(hexGrid.Coordinate), Is.EqualTo(0));
        }

        [Test]
        public void Can_apply_directions_to_new_HexGrid_example_3()
        {
            var hexGrid = new HexGrid();
            hexGrid.ApplyDirections("ne,ne,s,s");
            Assert.That(hexGrid.Coordinate.Q, Is.EqualTo(2));
            Assert.That(hexGrid.Coordinate.R, Is.EqualTo(0));
            Assert.That(hexGrid.Coordinate.S, Is.EqualTo(-2));
            Assert.That(hexGrid.GetStepsFromOrigin(hexGrid.Coordinate), Is.EqualTo(2));
        }

        [Test]
        public void Can_apply_directions_to_new_HexGrid_example_4()
        {
            var hexGrid = new HexGrid();
            hexGrid.ApplyDirections("se,sw,se,sw,sw");
            Assert.That(hexGrid.Coordinate.Q, Is.EqualTo(-1));
            Assert.That(hexGrid.Coordinate.R, Is.EqualTo(3));
            Assert.That(hexGrid.Coordinate.S, Is.EqualTo(-2));
            Assert.That(hexGrid.GetStepsFromOrigin(hexGrid.Coordinate), Is.EqualTo(3));
        }

        [Test]
        public void Can_solve_2017_day_11_part_1()
        {
            var hexGrid = new HexGrid();
            hexGrid.ApplyDirections(HexGridPuzzleInput.GetPuzzleInput());
            Assert.That(hexGrid.Coordinate.Q, Is.EqualTo(-484));
            Assert.That(hexGrid.Coordinate.R, Is.EqualTo(808));
            Assert.That(hexGrid.Coordinate.S, Is.EqualTo(-324));
            Assert.That(hexGrid.GetStepsFromOrigin(hexGrid.Coordinate), Is.EqualTo(808)); // Part 1
            Assert.That(hexGrid.FarthestAway, Is.EqualTo(1556)); // Part 2
        }
    }
}
