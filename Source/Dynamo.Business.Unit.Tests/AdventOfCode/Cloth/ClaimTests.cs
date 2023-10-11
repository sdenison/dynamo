using Dynamo.Business.Shared.AdventOfCode.Cloth;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Cloth
{
    [TestFixture]
    internal class ClaimTests
    {
        [Test]
        public void Can_parse_claim_string()
        {
            var claimString = "#123 @ 3,2: 5x4";
            var claim = Claim.Parse(claimString);
            Assert.AreEqual(123, claim.Id);
            Assert.AreEqual(3, claim.X);
            Assert.AreEqual(2, claim.Y);
            Assert.AreEqual(5, claim.Width);
            Assert.AreEqual(4, claim.Height);
        }
    }
}
