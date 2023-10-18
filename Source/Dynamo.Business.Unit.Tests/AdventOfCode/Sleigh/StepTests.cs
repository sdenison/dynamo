using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Dynamo.Business.Shared.AdventOfCode.Sleigh;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Sleigh
{
    [TestFixture]
    public class StepTests
    {
        [Test]
        public void Can_parse_step_string()
        {
            string stepString = "Step C must be finished before step A can begin.";
            var instructions = new Instructions();
            instructions.AddInstruction(stepString);
            Assert.AreEqual("C", instructions.Steps.ToList()[0].StepName);
            Assert.AreEqual("A", instructions.Steps.ToList()[0].Steps.ToList()[0].StepName);
        }

        [Test]
        public void Can_get_steps_for_test_data()
        {
            var steps = GetTestData();
            var instructions = new Instructions();
            instructions.AddInstructions(steps);
            Assert.AreEqual("C", instructions.Steps.ToList()[0].StepName);
            Assert.AreEqual("A", instructions.Steps.ToList()[0].Steps.ToList()[0].StepName);
            Assert.AreEqual("B", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName);
            Assert.AreEqual("D", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName);
            Assert.AreEqual("F", instructions.Steps.ToList()[0].Steps.ToList()[1].StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName);
        }

        private string[] GetTestData()
        {
            return new string[]
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin.",
            };
        }
    }
}

