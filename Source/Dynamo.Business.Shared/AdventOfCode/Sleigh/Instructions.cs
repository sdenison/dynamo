using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Instructions
    {
        public SortedSet<Step> Steps { get; set; } = new SortedSet<Step>();

        public void AddInstruction(string instruction)
        {
            var blockingStepName = instruction.Split(' ')[1];
            var stepName = instruction.Split(' ')[7];

            Step existingBlockingStep = null;
            foreach (var step in Steps)
            {
                existingBlockingStep = step.GetStep(blockingStepName);
                if (existingBlockingStep != null)
                    break;
            }

            if (existingBlockingStep == null)
            {
                existingBlockingStep = new Step(blockingStepName);
            }

            Step existingBlockedStep = null;
            foreach (var step in Steps)
            {
                existingBlockedStep = step.GetStep(stepName);
                if (existingBlockedStep != null)
                    break;
            }
            if (existingBlockedStep == null)
                existingBlockedStep = new Step(blockingStepName);


        }

        public void AddInstructions(string[] instructions)
        {
            foreach (string instruction in instructions)
            {
                AddInstruction(instruction);
            }
        }
    }
}
