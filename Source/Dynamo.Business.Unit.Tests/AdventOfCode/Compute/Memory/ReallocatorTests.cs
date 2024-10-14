using Dynamo.Business.Shared.AdventOfCode.Compute.Memory;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Memory
{
    [TestFixture]
    public class ReallocatorTests
    {

        [Test]
        public void Can_create_Reallocator()
        {
            var blocks = "0\t2\t7\t0";
            var reallocator = new Reallocator(blocks);
            Assert.That(reallocator.Blocks.Blocks.Count(), Is.EqualTo(4));
            var cycles = reallocator.Reallocate();
            Assert.That(cycles, Is.EqualTo(5));
        }

        [Test]
        public void Can_create_Reallocator_with_puzzle_data()
        {
            var blocks = "10\t3\t15\t10\t5\t15\t5\t15\t9\t2\t5\t8\t5\t2\t3\t6";
            var reallocator = new Reallocator(blocks);
            Assert.That(reallocator.Blocks.Blocks.Count(), Is.EqualTo(16));
            var cycles = reallocator.Reallocate();
            Assert.That(cycles, Is.EqualTo(14029));
        }

        [Test]
        public void Can_create_Reallocator_with_part_2_puzzle_data()
        {
            var blocks = "10\t3\t15\t10\t5\t15\t5\t15\t9\t2\t5\t8\t5\t2\t3\t6";
            var reallocator = new Reallocator(blocks);
            Assert.That(reallocator.Blocks.Blocks.Count(), Is.EqualTo(16));
            var cycles = reallocator.ReallocateFiniteLoopSize();
            Assert.That(cycles, Is.EqualTo(2765));
        }
    }
}
