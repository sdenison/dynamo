using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Instructions
    {
        public SortedSet<Step> Steps { get; set; } = new SortedSet<Step>();

        public void AddInstruction(string instruction, int addedSeconds = 0)
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
                existingBlockingStep = new Step(blockingStepName, addedSeconds);
                Steps.Add(existingBlockingStep);
            }

            Step existingBlockedStep = null;
            foreach (var step in Steps)
            {
                existingBlockedStep = step.GetStep(stepName);
                if (existingBlockedStep != null)
                    break;
            }
            existingBlockedStep ??= new Step(stepName, addedSeconds);
            existingBlockedStep.BlockedBySteps.Add(existingBlockingStep);
            existingBlockingStep.Steps.Add(existingBlockedStep);
        }

        public string GetNextStepName()
        {
            List<Step> stepsThatCanRun = Steps.ToList()[0].GetStepsThatCanRun();
            List<string> stepsThatCanRunString = stepsThatCanRun.OrderBy(x => x.StepName).Select(x => x.StepName).Distinct().ToList();
            return stepsThatCanRunString[0];
        }

        public Step GetNextStep()
        {
            List<Step> stepsThatCanRun = new List<Step>();
            foreach (var step in Steps)
                stepsThatCanRun.AddRange(step.GetStepsThatCanRun());
            List<Step> stepsThatCanRunOrdered = stepsThatCanRun.OrderBy(x => x.StepName).ToList();
            if (stepsThatCanRunOrdered.Count == 0)
                return null;
            return stepsThatCanRunOrdered[0];
        }

        public string GetStepNamesInOrder()
        {
            StringBuilder stepNames = new StringBuilder();
            while (GetNextStep() != null)
            {
                var nextStep = GetNextStep();
                stepNames.Append(nextStep.StepName);
                nextStep.Run();
            }
            return stepNames.ToString();
        }


        public List<Step> GetNextSteps()
        {
            List<Step> stepsThatCanRun = new List<Step>();
            foreach (var step in Steps)
                stepsThatCanRun.AddRange(step.GetStepsThatCanRun());
            List<Step> stepsThatCanRunOrdered = stepsThatCanRun.OrderBy(x => x.StepName).ToList();
            if (stepsThatCanRunOrdered.Count == 0)
                return null;
            return stepsThatCanRunOrdered;
        }

        public int GetSecondsTakenToRun(int addedSeconds, int numberOfWorkers)
        {
            List<Worker> workers = new List<Worker>();
            for (int i = 0; i < numberOfWorkers; i++)
                workers.Add(new Worker());

            var secondsRunning = 0;
            while (GetNextSteps() != null)
            {
                var nextSteps = GetNextSteps().Where(x => x.IsRunning == false);
                foreach (var step in nextSteps)
                {
                    var foundWorker = false;
                    foreach (var worker in workers)
                    {
                        if (worker.Done())
                        {
                            worker.StartWork(step);
                            break;
                        }
                    }
                }
                secondsRunning++;
                foreach (var worker in workers)
                    worker.TakeStep();
            }
            return secondsRunning;
        }

        //public Step Get

        public void AddInstructions(string[] instructions, int addedSeconds = 0)
        {
            foreach (string instruction in instructions)
            {
                AddInstruction(instruction, addedSeconds);
            }
        }
    }
}
