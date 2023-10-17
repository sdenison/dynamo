using System;
namespace Dynamo.Business.Shared.AdventOfCode.Guard
{
    public class GuardEvent : IComparable
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public DateTime EventDateTime { get; }
        public EventType EventType { get; }
        public int GuardId { get; set; }

        public GuardEvent(string eventString)
        {
            Year = int.Parse(eventString.Substring(1, 4));
            Month = int.Parse(eventString.Substring(6, 2));
            Day = int.Parse(eventString.Substring(9, 2));
            Hour = int.Parse(eventString.Substring(12, 2));
            Minute = int.Parse(eventString.Substring(15, 2));
            EventDateTime = new DateTime(Year, Month, Day, Hour, Minute, 0);
            var eventType = eventString.Substring(19);
            if (eventType.StartsWith("Guard"))
            {
                EventType = EventType.BeginShift;
                GuardId = int.Parse(eventType.Split(' ')[1].Replace("#", ""));
            }
            else if (eventType.Equals("falls asleep"))
                EventType = EventType.FallAsleep;
            else if (eventType.StartsWith("wakes up"))
                EventType = EventType.WakeUp;
            else
                throw new ArgumentException($"eventString {eventString} could not be parsed");
        }

        public int CompareTo(object obj)
        {
            var otherGuardEvent = (GuardEvent)obj;
            if (Month < otherGuardEvent.Month) return -1;
            if (Month > otherGuardEvent.Month) return 1;
            if (Day < otherGuardEvent.Day) return -1;
            if (Day > otherGuardEvent.Day) return 1;
            if (Hour < otherGuardEvent.Hour) return -1;
            if (Hour > otherGuardEvent.Hour) return 1;
            if (Minute < otherGuardEvent.Minute) return -1;
            if (Minute > otherGuardEvent.Minute) return 1;
            return 0;
        }
    }

    public enum EventType
    {
        BeginShift,
        FallAsleep,
        WakeUp
    }
}
