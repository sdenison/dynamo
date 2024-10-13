using Dynamo.Business.Shared.AdventOfCode.Compute.Memory;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Memory
{
    [TestFixture]
    public class SpiralMemoryTests
    {
        [TestCase(1, 0)]
        [TestCase(12, 3)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        public void Can_get_manhaattan_distance(int memorySquare, int manhattanDistance)
        {
            var incrementedMemory = new IncrementedMemory();
            var memory = incrementedMemory.Generate(memorySquare);
            var finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.ManhattanDistance, Is.EqualTo(manhattanDistance));
        }

        [Test]
        public void Can_get_correct_x_and_y_coordinates()
        {
            var spiralMemory = new IncrementedMemory();
            var memory = spiralMemory.Generate(2);
            var finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(1));
            Assert.That(finalPoint.Y, Is.EqualTo(0));
            memory = spiralMemory.Generate(3);
            finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(1));
            Assert.That(finalPoint.Y, Is.EqualTo(1));
            Assert.That(finalPoint.Value, Is.EqualTo(3));
            memory = spiralMemory.Generate(5);
            finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(-1));
            Assert.That(finalPoint.Y, Is.EqualTo(1));
            Assert.That(finalPoint.Value, Is.EqualTo(5));
            memory = spiralMemory.Generate(7);
            finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(-1));
            Assert.That(finalPoint.Y, Is.EqualTo(-1));
            Assert.That(finalPoint.Value, Is.EqualTo(7));
        }

        [Test]
        public void Can_solve_2017_day_3_part_1()
        {
            var incrementedMemory = new IncrementedMemory();
            var memory = incrementedMemory.Generate(347991);
            var finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.ManhattanDistance, Is.EqualTo(480));
            Assert.That(memory.Count, Is.EqualTo(347991));
        }

        [Test]
        public void Can_get_correct_x_and_y_coordinates_with_summed_values()
        {
            var summedMemory = new SummedMemory();
            var memory = summedMemory.Generate(2);
            var finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(0));
            Assert.That(finalPoint.Y, Is.EqualTo(1));
            Assert.That(finalPoint.Value, Is.EqualTo(4));
            memory = summedMemory.Generate(4);
            finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(-1));
            Assert.That(finalPoint.Y, Is.EqualTo(1));
            Assert.That(finalPoint.Value, Is.EqualTo(5));
            memory = summedMemory.Generate(142);
            finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.X, Is.EqualTo(-2));
            Assert.That(finalPoint.Y, Is.EqualTo(2));
            Assert.That(finalPoint.Value, Is.EqualTo(147));
        }

        [Test]
        public void Can_solve_2017_day_3_part_2()
        {
            var summedMemory = new SummedMemory();
            var memory = summedMemory.Generate(347991);
            var finalPoint = memory.FindLast(x => true);
            Assert.That(finalPoint.Value, Is.EqualTo(349975));
        }
    }
}
