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
            var layer = new Layer(0, 4);
            Assert.That(layer.LayerId, Is.EqualTo(0));
            Assert.That(layer.CurrentDepth, Is.EqualTo(1));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(2));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(3));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(4));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(3));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(2));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(1));
            layer.AdvanceOnePicosecond();
            Assert.That(layer.CurrentDepth, Is.EqualTo(2));

        }
    }
}
