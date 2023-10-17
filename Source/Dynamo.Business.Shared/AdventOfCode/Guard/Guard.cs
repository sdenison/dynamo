using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Guard
{
    public class Guard
    {
        public int GuardId { get; }
        public Dictionary<Time, int> Sleeping { get; set; } = new Dictionary<Time, int>();
        public List<GuardEvent> GuardEvents { get; }

        public Guard(int guardId)
        {
            GuardId = guardId;
            GuardEvents = new List<GuardEvent>();
        }

        public int GetMinutesAsleep()
        {
            var minutesAsleep = 0;
            foreach (var time in Sleeping.Keys)
                minutesAsleep += Sleeping[time];
            return minutesAsleep;
        }

        public Time TimeTheySleptTheMost()
        {
            var maxTimesSlept = 0;
            Time timeTheySleptTheMost = null;// = Sleeping.Keys.First();

            foreach (var time in Sleeping.Keys)
            {
                if (Sleeping[time] > maxTimesSlept)
                {
                    maxTimesSlept = Sleeping[time];
                    timeTheySleptTheMost = time;
                }
            }
            return timeTheySleptTheMost;
        }

        public void AddEvent(GuardEvent guardEvent)
        {
            if (guardEvent.EventType == EventType.WakeUp)
            {
                var lastSleep = GuardEvents[GuardEvents.Count - 1];
                if (lastSleep.EventType != EventType.FallAsleep)
                    throw new Exception("Last event should have been FallAsleep");

                TimeSpan minutesDifference = guardEvent.EventDateTime - lastSleep.EventDateTime;
                var currentMoment = lastSleep.EventDateTime;
                for (int minute = 0; minute < minutesDifference.TotalMinutes; minute++)
                {
                    var time = new Time(currentMoment.Hour, currentMoment.Minute);
                    if (Sleeping.ContainsKey(time))
                        Sleeping[time]++;
                    else
                        Sleeping.Add(time, 1);
                    currentMoment = currentMoment.AddMinutes(1);
                }
            }
            GuardEvents.Add(guardEvent);
        }
    }
}
