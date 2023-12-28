using System.ComponentModel;

namespace Dynamo.Business.Shared.Utilities
{
    public enum JobType
    {
        [Description("Day 2 Step 1")]
        Day2Step1,
        [Description("Day 2 Step 2")]
        Day2Step2,
        [Description("Day 3 Step 1")]
        Day3Step1,
        [Description("Day 3 Step 2")]
        Day3Step2,
        [Description("Day 4 Step 1")]
        Day4Step1,
        [Description("Day 4 Step 2")]
        Day4Step2,
        [Description("Day 7 Step 1")]
        Day7Step1,
        [Description("Day 7 Step 2")]
        Day7Step2,
        [Description("Day 8 Step 1")]
        Day8Step1,
        [Description("Day 8 Step 2")]
        Day8Step2,
        [Description("Day 9")]
        Day9,
        [Description("Day 11 Step 1")]
        Day11Step1,
        [Description("Day 11 Step 2")]
        Day11Step2,
        [Description("Day 13 Step 1")]
        Day13Step1,
        [Description("Day 13 Step 2")]
        Day13Step2,
        [Description("Busy Box")]
        BusyBox, //Used to test the system
    }
}
