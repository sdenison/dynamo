using System.Linq;
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
            var instructions = new JobRunner(stepString);
            Assert.That("C", Is.EqualTo(instructions.Steps.ToList()[0].StepName));
            Assert.That("A", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[0].StepName));
        }

        [Test]
        public void Can_get_steps_for_test_data()
        {
            var steps = GetTestData();
            var instructions = new JobRunner(steps);
            var cStep = instructions.Steps.First();
            Assert.That("C", Is.EqualTo(cStep.StepName));
            var aStep = cStep.Steps.First();
            Assert.That("A", Is.EqualTo(aStep.StepName));
            Assert.That("B", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName));
            Assert.That("E", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[0].StepName));
            Assert.That("D", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].StepName));
            Assert.That("E", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName));
            var fStep = cStep.Steps.ToList()[1];
            Assert.That("F", Is.EqualTo(fStep.StepName));
            Assert.That("E", Is.EqualTo(instructions.Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0].StepName));

            var eStep = instructions.Steps.ToList()[0].Steps.ToList()[1].Steps.ToList()[0];
            Assert.That("E", Is.EqualTo(eStep.StepName));
            Assert.That(3, Is.EqualTo(eStep.BlockedBySteps.Count));

            Assert.That(fStep.CanRun(), Is.False);
            Assert.That(cStep.CanRun(), Is.True);

            var nextStep = instructions.Step0.GetNextStep();
            Assert.That("C", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That("A", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That("B", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That("D", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That("F", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That("E", Is.EqualTo(nextStep.StepName));
            nextStep.Run();
            nextStep = instructions.Step0.GetNextStep();
            Assert.That(nextStep, Is.Null);
        }

        [Test]
        public void Can_make_string_of_steps_in_order()
        {
            var steps = GetTestData();
            var instructions = new JobRunner(steps);
            var stepNames = instructions.GetStepNamesInOrder();
            Assert.That("CABDFE", Is.EqualTo(stepNames));
        }

        [Test]
        public void Can_get_day_7_part_1_answer()
        {
            var steps = TestDataProvider.GetPuzzleInput();
            var instructions = new JobRunner(steps);
            var stepNames = instructions.GetStepNamesInOrder();
            Assert.That("HEGMPOAWBFCDITVXYZRKUQNSLJ", Is.EqualTo(stepNames));
        }

        [Test]
        public void Can_add_workers_and_variable_seconds_for_steps()
        {
            var steps = GetTestData();
            var instructions = new JobRunner(steps);
            var secondsTaken = instructions.GetSecondsTakenToRun(2);
            Assert.That(15, Is.EqualTo(secondsTaken));
        }

        [Test]
        public void Can_get_day_7_part_2_answer()
        {
            var steps = TestDataProvider.GetPuzzleInput();
            var instructions = new JobRunner(steps, 60);
            var secondsTaken = instructions.GetSecondsTakenToRun(5);
            Assert.That(1226, Is.EqualTo(secondsTaken));
        }

        private static string[] GetTestData()
        {
            return new[]
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

