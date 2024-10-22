using Dynamo.Business.Shared.AdventOfCode.Compute.Security;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Security
{
    [TestFixture]
    public class FirewallTests
    {
        [Test]
        public void Can_create_firewall_with_layers()
        {
            string[] layerStrings = new string[]
            {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"
            };
            var fireWall = new Firewall(layerStrings);
            Assert.That(fireWall.Layers.Count, Is.EqualTo(7));
            Assert.That(fireWall.Layers.All(x => x.SecurityScanDepth == 1));
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.Layers[0].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[1].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[4].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[6].SecurityScanDepth, Is.EqualTo(2));
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.Layers[0].SecurityScanDepth, Is.EqualTo(3));
            Assert.That(fireWall.Layers[1].SecurityScanDepth, Is.EqualTo(1));
            Assert.That(fireWall.Layers[4].SecurityScanDepth, Is.EqualTo(3));
            Assert.That(fireWall.Layers[6].SecurityScanDepth, Is.EqualTo(3));
        }
    }
}
