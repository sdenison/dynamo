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
            Assert.That(hasher.GetElement(0), Is.EqualTo(2));
            Assert.That(hasher.GetElement(1), Is.EqualTo(1));
            Assert.That(hasher.GetElement(2), Is.EqualTo(0));
            Assert.That(hasher.CurrentPosition, Is.EqualTo(3));
        }
    }
}
