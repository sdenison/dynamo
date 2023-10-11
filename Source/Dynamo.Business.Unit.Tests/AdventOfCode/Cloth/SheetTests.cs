using System.Collections.Generic;
using Dynamo.Business.Shared.AdventOfCode.Cloth;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Cloth
{
    [TestFixture]
    public class SheetTests
    {
        [Test]
        public void Can_get_list_of_coordinates_for_claim()
        {
            var claim = Claim.Parse("#1 @ 1,3: 4x4");
            var sheet = new Sheet();
            var coordinates = sheet.ApplyClaim(claim);
            Assert.AreEqual(16, coordinates.Count);
            Assert.AreEqual(1, coordinates[0].X);
            Assert.AreEqual(3, coordinates[0].Y);
            Assert.AreEqual(4, coordinates[15].X);
            Assert.AreEqual(6, coordinates[15].Y);
        }

        [Test]
        public void Can_get_list_of_coordinates_for_claim2()
        {
            var claim = Claim.Parse("#2 @ 3,1: 4x4");
            var sheet = new Sheet();
            var coordinates = sheet.ApplyClaim(claim);
            Assert.AreEqual(16, coordinates.Count);
            Assert.AreEqual(3, coordinates[0].X);
            Assert.AreEqual(1, coordinates[0].Y);
            Assert.AreEqual(6, coordinates[15].X);
            Assert.AreEqual(4, coordinates[15].Y);
        }

        [Test]
        public void Can_get_list_of_coordinates_for_claim3()
        {
            var claim = Claim.Parse("#3 @ 5,5: 2x2");
            var sheet = new Sheet();
            var coordinates = sheet.ApplyClaim(claim);
            Assert.AreEqual(4, coordinates.Count);
            Assert.AreEqual(5, coordinates[0].X);
            Assert.AreEqual(5, coordinates[0].Y);
            Assert.AreEqual(6, coordinates[3].X);
            Assert.AreEqual(6, coordinates[3].Y);
        }

        [Test]
        public void Test_hash_code()
        {
            var usedCoordinates = new HashSet<Coordinate>();
            var coordinate1 = new Coordinate(5, 5);
            var coordinate2 = new Coordinate(6, 6);
            var coordinate3 = new Coordinate(5, 5);
            usedCoordinates.Add(coordinate1);
            Assert.IsFalse(usedCoordinates.Contains(coordinate2));
            Assert.IsTrue(usedCoordinates.Contains(coordinate3));
        }

        [Test]
        public void Can_find_where_claims_overlap()
        {
            var claims = new List<Claim>()
            {
                Claim.Parse("#1 @ 1,3: 4x4"),
                Claim.Parse("#2 @ 3,1: 4x4"),
                Claim.Parse("#3 @ 5,5: 2x2")
            };
            var sheet = new Sheet();
            var overlappingClaims = sheet.FindOverlap(claims);
            Assert.AreEqual(4, overlappingClaims.Count);
            Assert.AreEqual(3, overlappingClaims[0].X);
            Assert.AreEqual(3, overlappingClaims[0].Y);
            Assert.AreEqual(4, overlappingClaims[3].X);
            Assert.AreEqual(4, overlappingClaims[3].Y);
        }

    }
}
