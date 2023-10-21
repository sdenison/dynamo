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
            var cStep = instructions.Steps.First();
            Assert.AreEqual("C", cStep.StepName);
            var aStep = cStep.Steps.First();
            Assert.AreEqual("A", aStep.StepName);
            Assert.AreEqual("B", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName);
            Assert.AreEqual("D", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName);
            var fStep = cStep.Steps.ToList()[1];
            Assert.AreEqual("F", fStep.StepName);
            Assert.AreEqual("E", instructions.Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName);

            var eStep = instructions.Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0];
            Assert.AreEqual("E", eStep.StepName);
            Assert.AreEqual(3, eStep.BlockedBySteps.Count);

            Assert.IsFalse(fStep.CanRun());
            Assert.IsTrue(cStep.CanRun());

            var nextStepThatCanRun = instructions.GetNextStepName();
            Assert.AreEqual("C", nextStepThatCanRun);

            var nextStep = instructions.GetNextStep();
            Assert.AreEqual("C", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.AreEqual("A", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.AreEqual("B", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.AreEqual("D", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.AreEqual("F", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.AreEqual("E", nextStep.StepName);
            nextStep.Run();
            nextStep = instructions.GetNextStep();
            Assert.IsNull(nextStep);
        }

        [Test]
        public void Can_make_string_of_steps_in_order()
        {
            var steps = GetTestData();
            var instructions = new Instructions();
            instructions.AddInstructions(steps);
            var stepNames = instructions.GetStepNamesInOrder();
            Assert.AreEqual("CABDFE", stepNames);
        }

        [Test]
        public void Can_get_day_7_part_1_answer()
        {
            var steps = TestDataProvider.GetPuzzleInput();
            var instructions = new Instructions();
            instructions.AddInstructions(steps);
            var stepNames = instructions.GetStepNamesInOrder();
            Assert.AreEqual("HEGMPOAWBFCDITVXYZRKUQNSLJ", stepNames);
        }

        [Test]
        public void Can_add_workers_and_variable_seconds_for_steps()
        {
            var steps = GetTestData();
            var instructions = new Instructions();
            instructions.AddInstructions(steps);
            var secondsTaken = instructions.GetSecondsTakenToRun(0, 2);
            Assert.AreEqual(15, secondsTaken);
        }

        [Test]
        public void Can_get_day_7_part_2_answer()
        {
            var steps = TestDataProvider.GetPuzzleInput();
            var instructions = new Instructions();
            instructions.AddInstructions(steps, 60);
            var secondsTaken = instructions.GetSecondsTakenToRun(60, 5);
            Assert.AreEqual(1226, secondsTaken);
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

