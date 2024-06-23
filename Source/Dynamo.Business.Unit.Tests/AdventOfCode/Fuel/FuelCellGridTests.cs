using Dynamo.Business.Shared.AdventOfCode.Fuel;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Fuel
{
    [TestFixture]
    public class FuelCellGridTests
    {
        [Test]
        public void Can_get_power_level_from_fuel_cell()
        {
            var fuelCell = new FuelCell(3, 5, 8);
            Assert.That(4, Is.EqualTo(fuelCell.Power));
        }

        [TestCase(122, 79, 57, -5)]
        [TestCase(217, 196, 39, 0)]
        [TestCase(101, 153, 71, 4)]
        public void Get_power_examples(int x, int y, int gridSerialNumber, int expectedPower)
        {
            var fuelCell = new FuelCell(x, y, gridSerialNumber);
            Assert.That(expectedPower, Is.EqualTo(fuelCell.Power));
        }

        [Test]
        public void Can_create_fuel_cell_grid()
        {
            var grid = new FuelCellGrid(300, 18);
            Assert.That(grid, Is.Not.Null);
            //Assert total number of FuelCells
            Assert.That(90000, Is.EqualTo(grid.FuelCells.GetLength(0) * grid.FuelCells.GetLength(1)));
        }

        [TestCase(18, 33, 45, 29)]
        [TestCase(42, 21, 61, 30)]
        public void Can_get_power_for_window(int gridSerialNumber, int x, int y, int expectedPower)
        {
            var grid = new FuelCellGrid(300, gridSerialNumber);
            Assert.That(grid, Is.Not.Null);
            var power = grid.GetPowerForWindow(x, y, 3);
            Assert.That(expectedPower, Is.EqualTo(power));
        }

        [Test]
        public void Can_get_max_power_coordinates()
        {
            var grid = new FuelCellGrid(300, 9306);
            var maxPowerIdentifier = grid.GetMaxPowerCoordinates(3);
            Assert.That(235, Is.EqualTo(maxPowerIdentifier.Coordinates.X));
            Assert.That(38, Is.EqualTo(maxPowerIdentifier.Coordinates.Y));
        }

        [Test, Ignore("takes too long for NCrunch")]
        public void Can_get_max_power()
        {
            var grid = new FuelCellGrid(300, 18);
            var maxPowerIdentifier = grid.GetMaxPower();
            Assert.That(1341075099, Is.EqualTo(grid.Calculations));
            Assert.That(90, Is.EqualTo(maxPowerIdentifier.Coordinates.X));
            Assert.That(269, Is.EqualTo(maxPowerIdentifier.Coordinates.Y));
            Assert.That(16, Is.EqualTo(maxPowerIdentifier.WindowSize));
            Assert.That(113, Is.EqualTo(maxPowerIdentifier.Power));
        }

        [Test, Ignore("takes too long for NCrunch")]
        public void Can_get_max_power2()
        {
            var grid = new FuelCellGrid(300, 42);
            var maxPowerIdentifier = grid.GetMaxPower();
            Assert.That(232, Is.EqualTo(maxPowerIdentifier.Coordinates.X));
            Assert.That(251, Is.EqualTo(maxPowerIdentifier.Coordinates.Y));
            Assert.That(12, Is.EqualTo(maxPowerIdentifier.WindowSize));
            Assert.That(119, Is.EqualTo(maxPowerIdentifier.Power));
        }

        [Test, Ignore("takes too long for NCrunch")]
        public void Get_day_11_part_2_answer()
        {
            var grid = new FuelCellGrid(300, 9306);
            var maxPowerIdentifier = grid.GetMaxPower();
            Assert.That(233, Is.EqualTo(maxPowerIdentifier.Coordinates.X));
            Assert.That(146, Is.EqualTo(maxPowerIdentifier.Coordinates.Y));
            Assert.That(13, Is.EqualTo(maxPowerIdentifier.WindowSize));
            Assert.That(95, Is.EqualTo(maxPowerIdentifier.Power));
        }
    }
}
