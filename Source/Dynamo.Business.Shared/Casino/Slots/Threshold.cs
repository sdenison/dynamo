using System;

namespace Dynamo.Business.Shared.Casino.Slots
{
    public class Threshold
    {
        public int WinAmount { get; set; }
        public int Percentage { get; set; }

        public Threshold(int winAmount, int percentage)
        {
            WinAmount = winAmount;
            Percentage = percentage;
        }


    }
}
