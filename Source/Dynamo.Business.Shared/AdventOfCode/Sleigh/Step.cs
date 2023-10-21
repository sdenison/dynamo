using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Step : IComparable<Step>
    {
        public SortedSet<Step?> Steps { get; set; } = new SortedSet<Step?>();
        public List<Step?> BlockedBySteps { get; set; } = new List<Step?>();
        public string StepName { get; set; }
        public int SecondsToRun { get; set; }
        public bool IsRunning { get; set; }
        public bool HasRun { get; set; } = false;

        public List<Step>? GetNextSteps()
        {
            var stepsThatCanRun = new List<Step>();
            foreach (var step in Steps)
                stepsThatCanRun.AddRange(step?.GetAllStepsThatCanRun());
            var stepsThatCanRunOrdered = stepsThatCanRun.OrderBy(x => x.StepName).ToList();
            return stepsThatCanRunOrdered.Count == 0 ? null : stepsThatCanRunOrdered;
        }

        public bool HasNextSteps()
        {
            var nextSteps = GetNextSteps();
            if (nextSteps == null || nextSteps.Count == 0) return false;
            return true;
        }

        public Step? GetNextStep()
        {
            var nextSteps = GetNextSteps();
            return nextSteps?[0];
        }

        public Step(string stepName, int addedSeconds = 0)
        {
            StepName = stepName;
            var ordinal = stepName.ToUpper()[0] - 'A' + 1;
            SecondsToRun = addedSeconds + ordinal;
            IsRunning = false;
        }

        public void Run()
        {
            HasRun = true;
            IsRunning = false;
        }

        public bool CanRun()
        {
            return BlockedBySteps.All(x => x != null && x.HasRun);
        }

        public List<Step> GetAllStepsThatCanRun()
        {
            List<Step> stepsThatCanRun = new List<Step>();
            if (HasRun == true)
                foreach (var step in this.Steps)
                    stepsThatCanRun.AddRange(step?.GetAllStepsThatCanRun());
            if (this.CanRun() && this.HasRun == false)
                stepsThatCanRun.Add(this);
            return stepsThatCanRun;
        }

        public Step? GetStep(string stepName)
        {
            if (StepName == stepName)
                return this;
            foreach (var step in Steps)
            {
                var innerStep = step?.GetStep(stepName);
                if (innerStep != null)
                    return innerStep;
            }
            return null;
        }

        public void AddInstruction(string instruction, int addedSeconds = 0)
        {
            var blockingStepName = instruction.Split(' ')[1];
            var stepName = instruction.Split(' ')[7];

            Step? existingBlockingStep = null;
            foreach (var step in Steps)
            {
                existingBlockingStep = step?.GetStep(blockingStepName);
                if (existingBlockingStep != null)
                    break;
            }

            if (existingBlockingStep == null)
            {
                existingBlockingStep = new Step(blockingStepName, addedSeconds);
                Steps.Add(existingBlockingStep);
            }

            Step? existingBlockedStep = null;
            foreach (var step in Steps)
            {
                existingBlockedStep = step?.GetStep(stepName);
                if (existingBlockedStep != null)
                    break;
            }
            existingBlockedStep ??= new Step(stepName, addedSeconds);
            existingBlockedStep.BlockedBySteps.Add(existingBlockingStep);
            existingBlockingStep.Steps.Add(existingBlockedStep);
        }

        public int CompareTo(Step other)
        {
            return string.Compare(StepName, other.StepName, StringComparison.Ordinal);
        }
    }
}
