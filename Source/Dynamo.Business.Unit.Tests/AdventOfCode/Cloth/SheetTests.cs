using System.Collections.Generic;
using System.Security;
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
            Assert.That(16, Is.EqualTo(coordinates.Count));
            Assert.That(1, Is.EqualTo(coordinates[0].X));
            Assert.That(3, Is.EqualTo(coordinates[0].Y));
            Assert.That(4, Is.EqualTo(coordinates[15].X));
            Assert.That(6, Is.EqualTo(coordinates[15].Y));
        }

        [Test]
        public void Can_get_list_of_coordinates_for_claim2()
        {
            var claim = Claim.Parse("#2 @ 3,1: 4x4");
            var sheet = new Sheet();
            var coordinates = sheet.ApplyClaim(claim);
            Assert.That(16, Is.EqualTo(coordinates.Count));
            Assert.That(3, Is.EqualTo(coordinates[0].X));
            Assert.That(1, Is.EqualTo(coordinates[0].Y));
            Assert.That(6, Is.EqualTo(coordinates[15].X));
            Assert.That(4, Is.EqualTo(coordinates[15].Y));
        }

        [Test]
        public void Can_get_list_of_coordinates_for_claim3()
        {
            var claim = Claim.Parse("#3 @ 5,5: 2x2");
            var sheet = new Sheet();
            var coordinates = sheet.ApplyClaim(claim);
            Assert.That(4, Is.EqualTo(coordinates.Count));
            Assert.That(5, Is.EqualTo(coordinates[0].X));
            Assert.That(5, Is.EqualTo(coordinates[0].Y));
            Assert.That(6, Is.EqualTo(coordinates[3].X));
            Assert.That(6, Is.EqualTo(coordinates[3].Y));
        }

        [Test]
        public void Test_hash_code()
        {
            var usedCoordinates = new HashSet<Coordinate>();
            var coordinate1 = new Coordinate(5, 5);
            var coordinate2 = new Coordinate(6, 6);
            var coordinate3 = new Coordinate(5, 5);
            usedCoordinates.Add(coordinate1);
            Assert.That(usedCoordinates.Contains(coordinate2), Is.False);
            Assert.That(usedCoordinates.Contains(coordinate3), Is.True);
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
            Assert.That(4, Is.EqualTo(overlappingClaims.Count));
            Assert.That(3, Is.EqualTo(overlappingClaims[0].X));
            Assert.That(3, Is.EqualTo(overlappingClaims[0].Y));
            Assert.That(4, Is.EqualTo(overlappingClaims[3].X));
            Assert.That(4, Is.EqualTo(overlappingClaims[3].Y));
        }

        [Test]
        public void Can_get_day_3_part_1_puzzle_answer()
        {
            var claims = TestDataProvider.GetClaims();
            var sheet = new Sheet();
            var overlappingClaims = sheet.FindOverlap(claims);
            Assert.That(111935, Is.EqualTo(overlappingClaims.Count));
        }

        [Test]
        public void Can_get_day_3_part_2_puzzle_answer()
        {
            var claims = TestDataProvider.GetClaims();
            var sheet = new Sheet();
            var claimsWithNoOverlap = sheet.FindClaimsWithNoOverlap(claims);
            Assert.That(1, Is.EqualTo(claimsWithNoOverlap.Count));
            Assert.That(650, Is.EqualTo(claimsWithNoOverlap[0].Id));
        }
    }
}
