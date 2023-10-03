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
            Assert.AreEqual(4, fuelCell.Power);
        }

        [TestCase(122, 79, 57, -5)]
        [TestCase(217, 196, 39, 0)]
        [TestCase(101, 153, 71, 4)]
        public void Get_power_examples(int x, int y, int gridSerialNumber, int expectedPower)
        {
            var fuelCell = new FuelCell(x, y, gridSerialNumber);
            Assert.AreEqual(expectedPower, fuelCell.Power);
        }

        [Test]
        public void Can_create_fuel_cell_grid()
        {
            var grid = new FuelCellGrid(300, 18);
            Assert.IsNotNull(grid);
            //Assert total number of FuelCells
            Assert.AreEqual(90000, grid.FuelCells.GetLength(0) * grid.FuelCells.GetLength(1));
        }

        [TestCase(18, 33, 45, 29)]
        [TestCase(42, 21, 61, 30)]
        public void Can_get_power_for_window(int gridSerialNumber, int x, int y, int expectedPower)
        {
            var grid = new FuelCellGrid(300, gridSerialNumber);
            Assert.IsNotNull(grid);
            //top-left corner 33,45 would be windowX, windowY of 34,46
            var windowX = x + 1;
            var windowY = y + 1;
            var power = grid.GetPowerForWindow(windowX, windowY);
            Assert.AreEqual(expectedPower, power);
        }

        [Test]
        public void Can_get_max_power_coordinates()
        {
            var grid = new FuelCellGrid(300, 9306);
            var coordinates = grid.GetMaxPowerCoordinates();
            Assert.AreEqual(235, coordinates.X);
            Assert.AreEqual(38, coordinates.Y);
        }
    }
}
