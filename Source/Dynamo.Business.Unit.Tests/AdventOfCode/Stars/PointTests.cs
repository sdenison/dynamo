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
            var pointString = "position=< 9,  1 > velocity=< 0,  2 >";
            var point = Point.Parse(pointString);
            Assert.AreEqual(9, point.X);
            Assert.AreEqual(1, point.Y);
            Assert.AreEqual(0, point.Velocity.X);
            Assert.AreEqual(2, point.Velocity.Y);
        }

        [Test]
        public void Can_parse_point_from_string2()
        {
            var pointString = "position=<-30580, -10081> velocity=< 3,  1>";
            var point = Point.Parse(pointString);
            Assert.AreEqual(-30580, point.X);
            Assert.AreEqual(-10081, point.Y);
            Assert.AreEqual(3, point.Velocity.X);
            Assert.AreEqual(1, point.Velocity.Y);
        }

        [Test]
        public void Can_parse_point_from_string3()
        {
            var pointString = "position=< 20678,  10405> velocity=<-2, -1>";
            var point = Point.Parse(pointString);
            Assert.AreEqual(20678, point.X);
            Assert.AreEqual(10405, point.Y);
            Assert.AreEqual(-2, point.Velocity.X);
            Assert.AreEqual(-1, point.Velocity.Y);
        }

        [Test]
        public void Can_step_through_seconds()
        {
            var pointString = "position=<3,  9> velocity=< 1, -2>";
            var point = Point.Parse(pointString);
            point.Step();
            point.Step();
            point.Step();
            Assert.AreEqual(6, point.X);
            Assert.AreEqual(3, point.Y);
        }
    }
}
