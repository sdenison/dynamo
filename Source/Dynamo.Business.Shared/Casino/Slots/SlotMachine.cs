using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.Slots
{
    public class SlotMachine
    {
        private List<Threshold> _thresholds;
        public long Money { get; private set; }

        public SlotMachine(List<Threshold> thresholds, long money = 0)
        {
            _thresholds = thresholds;
            Money = money;
        }

        public int PullHandle()
        {
            var random = new Random();
            var randomNumber = random.Next(1, 101);
            var currentPercentage = 0;
            Money -= 1;
            foreach (var threshold in _thresholds)
            {
                currentPercentage += threshold.Percentage;
                if (randomNumber <= currentPercentage)
                {
                    Money += threshold.WinAmount;
                    return threshold.WinAmount;
                }
            }

            return 0;
        }

        public void PullHandleNumberOfTimes(int timesToPullHandle)
        {
            for (var i = 0; i < timesToPullHandle; i++)
            {
                if (Money > 0)
                    PullHandle();
                else
                    return;
            }
        }

        public void InsertMoney(long money)
        {
            Money = money;
        }
    }
}
