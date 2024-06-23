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
            Assert.That(123, Is.EqualTo(claim.Id));
            Assert.That(3, Is.EqualTo(claim.X));
            Assert.That(2, Is.EqualTo(claim.Y));
            Assert.That(5, Is.EqualTo(claim.Width));
            Assert.That(4, Is.EqualTo(claim.Height));
        }
    }
}
