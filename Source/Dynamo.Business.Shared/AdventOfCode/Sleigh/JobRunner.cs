using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class JobRunner
    {
        public SortedSet<Step> Steps => Step0.Steps;
        public Step Step0 { get; set; } = new Step("0");

        public JobRunner(string[] instructions, int addedSeconds = 0)
        {
            foreach (string instruction in instructions)
                Step0.AddInstruction(instruction, addedSeconds);
        }

        public JobRunner(string instruction, int addedSeconds = 0)
        {
            Step0.AddInstruction(instruction, addedSeconds);
        }

        public string GetStepNamesInOrder()
        {
            StringBuilder stepNames = new StringBuilder();
            while (Step0.GetNextStep() != null)
            {
                var nextStep = Step0.GetNextStep();
                stepNames.Append(nextStep.StepName);
                nextStep.Run();
            }
            return stepNames.ToString();
        }

        public int GetSecondsTakenToRun(int numberOfWorkers)
        {
            //Sets up or worker queues
            var workers = new List<Worker>();
            for (int i = 0; i < numberOfWorkers; i++)
                workers.Add(new Worker());

            //Running steps on workers and timing how long it takes
            var secondsRunning = 0;
            while (Step0.HasNextSteps())
            {
                var nextSteps = Step0.GetNextSteps().Where(x => x.IsRunning == false);
                foreach (var step in nextSteps)
                    foreach (var worker in workers)
                        if (worker.IsReadyToWork())
                        {
                            worker.StartWork(step);
                            break;
                        }
                secondsRunning++;
                foreach (var worker in workers)
                    worker.TakeStep();
            }
            return secondsRunning;
        }
    }
}
