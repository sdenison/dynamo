using Dynamo.Business.Shared.AdventOfCode.Compute.Memory;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Memory
{
    [TestFixture]
    public class SpiralMemoryTests
    {
        //[TestCase(1, 0)]
        [TestCase(12, 3)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        public void Can_get_manhaattan_distance(int memorySquare, int manhattanDistance)
        {
            var memory = new SpiralMemory(memorySquare);
            Assert.That(memory.FinalPoint.ManhattanDistance, Is.EqualTo(manhattanDistance));
        }

        [TestCase]
        public void Can_get_correct_x_and_y_coordinates()
        {
            var memory = new SpiralMemory(2);
            Assert.That(memory.FinalPoint.X, Is.EqualTo(1));
            Assert.That(memory.FinalPoint.Y, Is.EqualTo(0));
            memory = new SpiralMemory(3);
            Assert.That(memory.FinalPoint.X, Is.EqualTo(1));
            Assert.That(memory.FinalPoint.Y, Is.EqualTo(1));
            Assert.That(memory.FinalPoint.Value, Is.EqualTo(3));
            memory = new SpiralMemory(5);
            Assert.That(memory.FinalPoint.X, Is.EqualTo(-1));
            Assert.That(memory.FinalPoint.Y, Is.EqualTo(1));
            Assert.That(memory.FinalPoint.Value, Is.EqualTo(5));
            memory = new SpiralMemory(7);
            Assert.That(memory.FinalPoint.X, Is.EqualTo(-1));
            Assert.That(memory.FinalPoint.Y, Is.EqualTo(-1));
            Assert.That(memory.FinalPoint.Value, Is.EqualTo(7));
        }

        [Test]
        public void Can_solve_2017_day_3_part_1()
        {
            var memory = new SpiralMemory(347991);
            Assert.That(memory.FinalPoint.ManhattanDistance, Is.EqualTo(480));
        }
    }
}
