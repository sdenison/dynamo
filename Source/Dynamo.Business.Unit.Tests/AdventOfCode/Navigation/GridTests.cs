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
            var grid = new Grid(testData);

            Assert.AreEqual("A", grid.GridPoints[0, 0].OwnedBy.Name);
            Assert.AreEqual("A", grid.GridPoints[1, 1].OwnedBy.Name);
            Assert.IsNull(grid.GridPoints[5, 0].OwnedBy);
            Assert.AreEqual("C", grid.GridPoints[6, 0].OwnedBy.Name);
            Assert.AreEqual("E", grid.GridPoints[5, 2].OwnedBy.Name);
            Assert.AreEqual("F", grid.GridPoints[8, 9].OwnedBy.Name);
            Assert.AreEqual("F", grid.GridPoints[9, 9].OwnedBy.Name);

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
    }
}
