using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Worker
    {
        public Step Step { get; set; }

        public Worker() { }

        public void StartWork(Step step)
        {
            Step = step;
            Step.IsRunning = true;
        }

        public bool Done()
        {
            if (Step == null)
                return true;
            if (Step.SecondsToRun == 0)
            {
                Step.Run();
                return true;
            }
            else
                return false;
        }

        public void TakeStep()
        {
            if (Step != null && Step.IsRunning && !Done())
            {
                Step.SecondsToRun -= 1;
                if (Step.SecondsToRun == 0)
                    Step.Run();
            }
        }
    }
}
