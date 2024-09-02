using Dynamo.Business.Shared.AdventOfCode.Stars;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Stars
{
    [TestFixture]
    public class PointTests
    {
        [Test]
        public void Can_parse_point_from_string()
        {
            var pointString = "position=< 9, 1 > velocity=< 0,  2 >";
            var point = Point.Parse(pointString);
            Assert.That(9, Is.EqualTo(point.X));
            Assert.That(1, Is.EqualTo(point.Y));
            Assert.That(0, Is.EqualTo(point.Velocity.X));
            Assert.That(2, Is.EqualTo(point.Velocity.Y));
        }

        [Test]
        public void Can_parse_point_from_string2()
        {
            var pointString = "position=<-30580, -10081> velocity=< 3,  1>";
            var point = Point.Parse(pointString);
            Assert.That(-30580, Is.EqualTo(point.X));
            Assert.That(-10081, Is.EqualTo(point.Y));
            Assert.That(3, Is.EqualTo(point.Velocity.X));
            Assert.That(1, Is.EqualTo(point.Velocity.Y));
        }

        [Test]
        public void Can_parse_point_from_string3()
        {
            var pointString = "position=< 20678, 10405> velocity=<-2, -1>";
            var point = Point.Parse(pointString);
            Assert.That(20678, Is.EqualTo(point.X));
            Assert.That(10405, Is.EqualTo(point.Y));
            Assert.That(-2, Is.EqualTo(point.Velocity.X));
            Assert.That(-1, Is.EqualTo(point.Velocity.Y));
        }

        [Test]
        public void Can_step_through_seconds()
        {
            var pointString = "position=<3, 9> velocity=< 1, -2>";
            var point = Point.Parse(pointString);
            point.Step();
            point.Step();
            point.Step();
            Assert.That(6, Is.EqualTo(point.X));
            Assert.That(3, Is.EqualTo(point.Y));
        }
    }
}
