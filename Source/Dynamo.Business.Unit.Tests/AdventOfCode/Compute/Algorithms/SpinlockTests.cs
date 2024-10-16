using Dynamo.Business.Shared.AdventOfCode.Compute.Algorithms;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Algorithms
{
    [TestFixture]
    public class SpinlockTests
    {
        [Test]
        public void Can_create_0_spinlock()
        {
            var spinlock = new Spinlock();
            Assert.That(spinlock.Value, Is.EqualTo(0));
            spinlock.MoveForward(3);
            Assert.That(spinlock.Value, Is.EqualTo(0));
        }

        [Test]
        public void Can_add_to_spinlock()
        {
            var spinlock = new Spinlock();
            Assert.That(spinlock.Value, Is.EqualTo(0));
            spinlock = spinlock.Add(3, 1);
            Assert.That(spinlock.Value, Is.EqualTo(1));
            Assert.That(spinlock.Previous.Value, Is.EqualTo(0));
            spinlock = spinlock.Add(3, 2);
            Assert.That(spinlock.Value, Is.EqualTo(2));
            Assert.That(spinlock.Previous.Value, Is.EqualTo(0));
            Assert.That(spinlock.Next.Value, Is.EqualTo(1));
            spinlock = spinlock.Add(3, 3);
            Assert.That(spinlock.Value, Is.EqualTo(3));
            Assert.That(spinlock.Previous.Value, Is.EqualTo(2));
            Assert.That(spinlock.Next.Value, Is.EqualTo(1));
            spinlock = spinlock.Add(3, 4);
            Assert.That(spinlock.Value, Is.EqualTo(4));
            Assert.That(spinlock.Previous.Value, Is.EqualTo(2));
            Assert.That(spinlock.Next.Value, Is.EqualTo(3));
        }

        [Test]
        public void Can_get_run_2017_times_with_step_size_3()
        {
            var spinlock = new Spinlock();
            var steps = 3;
            for (var i = 1; i <= 2017; i++)
            {
                spinlock = spinlock.Add(steps, i);
            }
            Assert.That(spinlock.Next.Value, Is.EqualTo(638));
        }

        [Test]
        public void Can_get_puzzle_answer_2017_day_17_part_1()
        {
            var spinlock = new Spinlock();
            var steps = 370;
            for (var i = 1; i <= 2017; i++)
            {
                spinlock = spinlock.Add(steps, i);
            }
            Assert.That(spinlock.Next.Value, Is.EqualTo(1244));
        }
    }
}
