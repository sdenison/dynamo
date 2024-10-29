using Dynamo.Business.Shared.AdventOfCode.Compute.Security;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Security
{
    [TestFixture]
    public class HasherTests
    {
        [Test]
        public void Can_apply_lengths_Hasher()
        {
            var hasher = new Hasher(5);
            Assert.That(hasher.Elements.Count, Is.EqualTo(5));
            Assert.That(hasher.GetElement(0), Is.EqualTo(0));
            Assert.That(hasher.GetElement(4), Is.EqualTo(4));
            Assert.That(hasher.GetElement(5), Is.EqualTo(0));
            Assert.That(hasher.GetElement(6), Is.EqualTo(1));
            Assert.That(hasher.GetElement(10), Is.EqualTo(0));
            hasher.ApplyLength(3);
            Assert.That(hasher.CurrentPosition, Is.EqualTo(3));
            Assert.That(hasher.GetElement(0), Is.EqualTo(2));
            Assert.That(hasher.GetElement(1), Is.EqualTo(1));
            Assert.That(hasher.GetElement(2), Is.EqualTo(0));
            Assert.That(hasher.CurrentPosition, Is.EqualTo(3));
            hasher.ApplyLength(4);
            Assert.That(hasher.GetElement(0), Is.EqualTo(4));
            Assert.That(hasher.GetElement(1), Is.EqualTo(3));
            Assert.That(hasher.GetElement(2), Is.EqualTo(0));
            Assert.That(hasher.GetElement(3), Is.EqualTo(1));
            Assert.That(hasher.GetElement(4), Is.EqualTo(2));
            Assert.That(hasher.CurrentPosition, Is.EqualTo(3));
            hasher.ApplyLength(1);
            Assert.That(hasher.GetElement(0), Is.EqualTo(4));
            Assert.That(hasher.GetElement(1), Is.EqualTo(3));
            Assert.That(hasher.GetElement(2), Is.EqualTo(0));
            Assert.That(hasher.GetElement(3), Is.EqualTo(1));
            Assert.That(hasher.GetElement(4), Is.EqualTo(2));
            Assert.That(hasher.CurrentPosition, Is.EqualTo(1));
            hasher.ApplyLength(5);
            Assert.That(hasher.GetElement(0), Is.EqualTo(3));
            Assert.That(hasher.GetElement(1), Is.EqualTo(4));
            Assert.That(hasher.GetElement(2), Is.EqualTo(2));
            Assert.That(hasher.GetElement(3), Is.EqualTo(1));
            Assert.That(hasher.GetElement(4), Is.EqualTo(0));
            Assert.That(hasher.CurrentPosition, Is.EqualTo(4));
        }

        [Test]
        public void Can_apply_array_of_lengths()
        {
            var hasher = new Hasher(5);
            int[] salt = [3, 4, 1, 5];
            var hash = hasher.GetHash(salt);
            Assert.That(hash, Is.EqualTo(12));
        }

        [Test]
        public void Can_get_2017_day_10_part_1()
        {
            var hasher = new Hasher(256);
            int[] salt = [227, 169, 3, 166, 246, 201, 0, 47, 1, 255, 2, 254, 96, 3, 97, 144];
            var hash = hasher.GetHash(salt);
            Assert.That(hash, Is.EqualTo(13760));
        }

        [Test]
        public void Can_create_knot_hash_for_1_2_3()
        {
            var hasher = new Hasher(256);
            var knotHash = hasher.GetKnotHash("1,2,3");
            Assert.That(knotHash, Is.EqualTo("3efbe78a8d82f29979031a4aa0b16a9d"));
        }

        [Test]
        public void Can_create_knot_hash_for_empty_string()
        {
            var hasher = new Hasher(256);
            var knotHash = hasher.GetKnotHash("");
            Assert.That(knotHash, Is.EqualTo("a2582a3a0e66e6e86e3812dcb672a272"));
        }

        [Test]
        public void Can_create_knot_hash_for_AoC_2017()
        {
            var hasher = new Hasher(256);
            var knotHash = hasher.GetKnotHash("AoC 2017");
            Assert.That(knotHash, Is.EqualTo("33efeb34ea91902bb2f59c9920caa6cd"));
        }

        [Test]
        public void Can_solve_2017_day_10_part_2()
        {
            var hasher = new Hasher(256);
            var knotHash = hasher.GetKnotHash("227,169,3,166,246,201,0,47,1,255,2,254,96,3,97,144");
            Assert.That(knotHash, Is.EqualTo("2da93395f1a6bb3472203252e3b17fe5"));
        }
    }
}
