using Dynamo.Business.Shared.AdventOfCode.Compute.Security;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Security
{
    [TestFixture]
    public class LayerTests
    {
        [Test]
        public void Can_advance_pico_seconds_for_layers()
        {
            var layer = new Layer(4);
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(1));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(2));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(3));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(4));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(3));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(2));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(1));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.SecurityScanDepth, Is.EqualTo(2));
        }
    }
}
