using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators;


namespace Dynamo.Business.Unit.Tests.AdventOfCode.DuelingGenerators
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void Can_generate_values()
        {
            var generatorA = new Generator(id: "A", multiplicationFactor: 16807, 65);
            var nextNumber = generatorA.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(1092455));
            nextNumber = generatorA.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(1181022009));
            nextNumber = generatorA.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(245556042));
            nextNumber = generatorA.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(1744312007));
            nextNumber = generatorA.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(1352636452));

            var generatorB = new Generator(id: "B", 48271, 8921);
            nextNumber = generatorB.NextNumber();
            nextNumber = generatorB.NextNumber();
            nextNumber = generatorB.NextNumber();
            nextNumber = generatorB.NextNumber();
            nextNumber = generatorB.NextNumber();
            Assert.That(nextNumber, Is.EqualTo(285222916));
        }
    }
}
