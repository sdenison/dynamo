namespace Dynamo.Business.Shared.AdventOfCode.Sleigh
{
    public class Worker
    {
        public Step? Step { get; set; }

        public bool IsReadyToWork()
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

        public void StartWork(Step step)
        {
            Step = step;
            Step.IsRunning = true;
        }

        public void TakeStep()
        {
            if (Step == null || !Step.IsRunning || IsReadyToWork()) return;
            Step.SecondsToRun -= 1;
            if (Step.SecondsToRun == 0)
                Step.Run();
        }
    }
}
