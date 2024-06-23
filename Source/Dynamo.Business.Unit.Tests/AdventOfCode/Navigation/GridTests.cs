using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.AdventOfCode.Navigation;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Navigation
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void Can_create_a_grid()
        {
            var testData = GetTestData();
            var coordinates = Grid.ParseCoordinates(testData);
            var grid = new Grid(coordinates);

            Assert.That("A", Is.EqualTo(grid.GridPoints[0, 0].OwnedBy.Name));
            Assert.That("A", Is.EqualTo(grid.GridPoints[1, 1].OwnedBy.Name));
            Assert.That(grid.GridPoints[5, 0].OwnedBy, Is.Null);
            Assert.That("C", Is.EqualTo(grid.GridPoints[6, 0].OwnedBy.Name));
            Assert.That("E", Is.EqualTo(grid.GridPoints[5, 2].OwnedBy.Name));
            Assert.That("F", Is.EqualTo(grid.GridPoints[8, 9].OwnedBy.Name));
            Assert.That(coordinates.Single(x => x.Name.Equals("A")).IsInfinite);
            Assert.That(coordinates.Single(x => x.Name.Equals("B")).IsInfinite);
            Assert.That(coordinates.Single(x => x.Name.Equals("C")).IsInfinite);
            Assert.That(coordinates.Single(x => x.Name.Equals("D")).IsInfinite, Is.False);
            Assert.That(coordinates.Single(x => x.Name.Equals("E")).IsInfinite, Is.False);
            Assert.That(17, Is.EqualTo(grid.GetArea(coordinates.Single(x => x.Name == "E"))));
            Assert.That(17, Is.EqualTo(grid.GetLargesAreaNotInfinite()));
        }

        [Test]
        public void Solve_day_6_part_1()
        {
            var testData = GetPuzzleData();
            var coordinates = Grid.ParseCoordinates(testData);
            var grid = new Grid(coordinates);
            Assert.That(4215, Is.EqualTo(grid.GetLargesAreaNotInfinite()));
        }

        [Test]
        public void Can_get_area_where_close_to_coordinates()
        {
            var testData = GetTestData();
            var coordinates = Grid.ParseCoordinates(testData);
            var grid = new Grid(coordinates);
            Assert.That(16, Is.EqualTo(grid.GetAreaWithGridPointsInDistance(32)));
        }

        [Test]
        public void Solve_day_6_part_2()
        {
            var testData = GetPuzzleData();
            var coordinates = Grid.ParseCoordinates(testData);
            var grid = new Grid(coordinates);
            Assert.That(40376, Is.EqualTo(grid.GetAreaWithGridPointsInDistance(10000)));
        }

        private static string[] GetTestData()
        {
            return new string[]
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"
            };
        }

        private static string[] GetPuzzleData()
        {
            return new string[]
            {
                "158, 163",
                "287, 68",
                "76, 102",
                "84, 244",
                "162, 55",
                "272, 335",
                "345, 358",
                "210, 211",
                "343, 206",
                "219, 323",
                "260, 238",
                "83, 94",
                "137, 340",
                "244, 172",
                "335, 307",
                "52, 135",
                "312, 109",
                "276, 93",
                "288, 274",
                "173, 211",
                "125, 236",
                "200, 217",
                "339, 56",
                "286, 134",
                "310, 192",
                "169, 192",
                "313, 106",
                "331, 186",
                "40, 236",
                "194, 122",
                "244, 76",
                "159, 282",
                "161, 176",
                "262, 279",
                "184, 93",
                "337, 284",
                "346, 342",
                "283, 90",
                "279, 162",
                "112, 244",
                "49, 254",
                "63, 176",
                "268, 145",
                "334, 336",
                "278, 176",
                "353, 135",
                "282, 312",
                "96, 85",
                "90, 105",
                "354, 312",
            };
        }
    }
}
