using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Guard
{
    public class GuardEventTimeline
    {
        public SortedSet<GuardEvent> GuardEvents { get; set; } = new SortedSet<GuardEvent>();
        public List<Guard> Guards { get; set; } = new List<Guard>();

        public GuardEventTimeline(string[] eventStrings)
        {
            foreach (var eventString in eventStrings)
            {
                GuardEvents.Add(new GuardEvent(eventString));
            }
            var currentGuardId = 0;
            foreach (var guardEvent in GuardEvents)
            {
                if (guardEvent.EventType == EventType.BeginShift)
                {
                    if (Guards.All(x => x.GuardId != guardEvent.GuardId))
                        Guards.Add(new Guard(guardEvent.GuardId));
                    currentGuardId = guardEvent.GuardId;
                }

                var guard = Guards.Single(x => x.GuardId == currentGuardId);
                guard.AddEvent(guardEvent);
            }
        }

        public Guard GetGuardWithMostMinutesAsleep()
        {
            var mostMinutesAsleep = 0;
            Guard guardWithMostMinutesAsleep = Guards[0];
            foreach (var guard in Guards)
            {
                if (guard.GetMinutesAsleep() > guardWithMostMinutesAsleep.GetMinutesAsleep())
                    guardWithMostMinutesAsleep = guard;
            }
            return guardWithMostMinutesAsleep;
        }

        public Guard GetGuardWithMostMinutesAsleepForOneTime()
        {
            var currentGuard = Guards[0];
            var maxSleepInOneMinute = 0;
            foreach (var guard in Guards)
            {
                var timeTheySleptMost = guard.TimeTheySleptTheMost();
                if (timeTheySleptMost != null)
                    if (maxSleepInOneMinute < guard.Sleeping[timeTheySleptMost])
                    {
                        maxSleepInOneMinute = guard.Sleeping[timeTheySleptMost];
                        currentGuard = guard;
                    }
            }
            return currentGuard;
        }
    }
}
