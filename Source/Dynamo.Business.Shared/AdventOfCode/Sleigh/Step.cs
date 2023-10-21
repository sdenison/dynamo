using System;
using System.Collections.Generic;
using System.Linq;
namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Step : IComparable<Step>

    {
        public SortedSet<Step> Steps { get; set; } = new SortedSet<Step>();
        public List<Step> BlockedBySteps { get; set; } = new List<Step>();
        public string StepName { get; set; }
        public int SecondsToRun { get; set; }
        public bool IsRunning { get; set; }
        public bool HasRun { get; set; } = false;

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
            return BlockedBySteps.All(x => x.HasRun);
        }

        public List<Step> GetStepsThatCanRun()
        {
            List<Step> stepsThatCanRun = new List<Step>();
            if (HasRun == true)
            {
                foreach (var step in this.Steps)
                    stepsThatCanRun.AddRange(step.GetStepsThatCanRun());
            }
            if (this.CanRun() && this.HasRun == false)
            {
                stepsThatCanRun.Add(this);
            }
            return stepsThatCanRun;
        }


        public Step GetStep(string stepName)
        {
            if (StepName == stepName)
                return this;
            foreach (var step in Steps)
            {
                var innerStep = step.GetStep(stepName);
                if (innerStep != null)
                    return innerStep;
            }
            return null;
        }

        public int CompareTo(Step other)
        {
            return String.Compare(StepName, other.StepName, StringComparison.Ordinal);
        }
    }
}
