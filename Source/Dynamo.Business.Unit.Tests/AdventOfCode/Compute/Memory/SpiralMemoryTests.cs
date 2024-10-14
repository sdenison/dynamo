using Dynamo.Business.Shared.AdventOfCode.Compute.Memory;
using NUnit.Framework;
using System;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Memory
{
    [TestFixture]
    public class SpiralMemoryTests
    {
        [TestCase(1, 0)]
        [TestCase(12, 3)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        public void Can_get_manhaattan_distance(int memorySquare, int expectedManhattanDistance)
        {
            var incrementedMemory = new IncrementedMemory();
            var memory = incrementedMemory.Generate(memorySquare);
            var finalPoint = memory.Keys.Last();
            var manhattanDistance = Math.Abs(finalPoint.x) + Math.Abs(finalPoint.y);
            Assert.That(manhattanDistance, Is.EqualTo(expectedManhattanDistance));
        }

        [Test]
        public void Can_get_correct_x_and_y_coordinates()
        {
            var spiralMemory = new IncrementedMemory();
            var memory = spiralMemory.Generate(2);
            var finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(1));
            Assert.That(finalPoint.y, Is.EqualTo(0));
            memory = spiralMemory.Generate(3);
            finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(1));
            Assert.That(finalPoint.y, Is.EqualTo(1));
            Assert.That(memory[finalPoint], Is.EqualTo(3));
            memory = spiralMemory.Generate(5);
            finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(-1));
            Assert.That(finalPoint.y, Is.EqualTo(1));
            Assert.That(memory[finalPoint], Is.EqualTo(5));
            memory = spiralMemory.Generate(7);
            finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(-1));
            Assert.That(finalPoint.y, Is.EqualTo(-1));
            Assert.That(memory[finalPoint], Is.EqualTo(7));
        }

        [Test]
        public void Can_solve_2017_day_3_part_1()
        {
            var incrementedMemory = new IncrementedMemory();
            var memory = incrementedMemory.Generate(347991);
            var finalPoint = memory.Keys.Last();
            var manhattanDistance = Math.Abs(finalPoint.x) + Math.Abs(finalPoint.y);
            Assert.That(manhattanDistance, Is.EqualTo(480));
            Assert.That(memory.Count, Is.EqualTo(347991));
        }

        [Test]
        public void Can_get_correct_x_and_y_coordinates_with_summed_values()
        {
            var summedMemory = new SummedMemory();
            var memory = summedMemory.Generate(2);
            var finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(0));
            Assert.That(finalPoint.y, Is.EqualTo(1));
            Assert.That(memory[finalPoint], Is.EqualTo(4));
            memory = summedMemory.Generate(4);
            finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(-1));
            Assert.That(finalPoint.y, Is.EqualTo(1));
            Assert.That(memory[finalPoint], Is.EqualTo(5));
            memory = summedMemory.Generate(142);
            finalPoint = memory.Keys.Last();
            Assert.That(finalPoint.x, Is.EqualTo(-2));
            Assert.That(finalPoint.y, Is.EqualTo(2));
            Assert.That(memory[finalPoint], Is.EqualTo(147));
        }

        [Test]
        public void Can_solve_2017_day_3_part_2()
        {
            var summedMemory = new SummedMemory();
            var memory = summedMemory.Generate(347991);
            var finalPoint = memory.Keys.Last();
            Assert.That(memory[finalPoint], Is.EqualTo(349975));
        }
    }
}
