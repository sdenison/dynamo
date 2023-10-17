using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Guard
{
    public class GuardEventTimeline
    {
        public SortedSet<GuardEvent> GuardEvents { get; set; } = new SortedSet<GuardEvent>();

        public GuardEventTimeline(string[] eventStrings)
        {
            foreach (var eventString in eventStrings)
            {
                GuardEvents.Add(new GuardEvent(eventString));
            }
        }
    }
}
