using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Step : IComparable<Step>

    {
        public SortedSet<Step> Steps { get; set; } = new SortedSet<Step>();
        public string StepName { get; set; }

        public Step(string stepName, Step blockedStep)
        {
            StepName = stepName;
            Steps.Add(blockedStep);
        }

        public Step(string stepName)
        {
            StepName = stepName;
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
            return StepName.CompareTo(other.StepName);
        }
    }
}
