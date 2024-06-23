using System;
using System.Linq;
using Dynamo.Business.Shared.AdventOfCode.Guard;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Guard
{
    [TestFixture]
    public class EventTests
    {
        [Test]
        public void Can_parse_begin_shift()
        {
            var eventString = "[1518-11-01 00:00] Guard #10 begins shift";
            var guardEvent = new GuardEvent(eventString);

            Assert.That(1518, Is.EqualTo(guardEvent.Year));
            Assert.That(11, Is.EqualTo(guardEvent.Month));
            Assert.That(1, Is.EqualTo(guardEvent.Day));
            Assert.That(0, Is.EqualTo(guardEvent.Hour));
            Assert.That(0, Is.EqualTo(guardEvent.Minute));
            Assert.That(EventType.BeginShift, Is.EqualTo(guardEvent.EventType));
            Assert.That(10, Is.EqualTo(guardEvent.GuardId));
        }

        [Test]
        public void Can_get_timeline()
        {
            var timeLine = new GuardEventTimeline(GetEventStringsInOrder());
            Assert.That(17, Is.EqualTo(timeLine.GuardEvents.Count));
        }

        [Test]
        public void Can_get_timeline_out_of_order()
        {
            var timeLine = new GuardEventTimeline(GetEventStringsOutOfOrder());
            Assert.That(17, Is.EqualTo(timeLine.GuardEvents.Count));
            Assert.That(0, Is.EqualTo(timeLine.GuardEvents.ToList()[0].Hour));
            Assert.That(0, Is.EqualTo(timeLine.GuardEvents.ToList()[0].Minute));
            Assert.That(0, Is.EqualTo(timeLine.GuardEvents.ToList()[16].Hour));
            Assert.That(55, Is.EqualTo(timeLine.GuardEvents.ToList()[16].Minute));
        }

        [Test]
        public void Can_create_date_in_1500s()
        {
            var datetime = new DateTime(1518, 6, 16);
            Assert.That(1518, Is.EqualTo(datetime.Year));
        }

        [Test]
        public void Can_get_example_answer()
        {
            var timeline = new GuardEventTimeline(GetEventStringsOutOfOrder());
            var guardThatSleptTheMost = timeline.GetGuardWithMostMinutesAsleep();
            Assert.That(10, Is.EqualTo(guardThatSleptTheMost.GuardId));
            var timeTheySleptTheMost = guardThatSleptTheMost.TimeTheySleptTheMost();
            Assert.That(24, Is.EqualTo(timeTheySleptTheMost.Minute));
        }

        [Test]
        public void Can_get_Day_4_part_1_answer()
        {
            var timeline = new GuardEventTimeline(TestDataProvider.GetPuzzleInput());
            var guardThatSleptTheMost = timeline.GetGuardWithMostMinutesAsleep();
            Assert.That(863, Is.EqualTo(guardThatSleptTheMost.GuardId));
            var timeTheySleptTheMost = guardThatSleptTheMost.TimeTheySleptTheMost();
            Assert.That(46, Is.EqualTo(timeTheySleptTheMost.Minute));
            //Puzzle answerr is 863 * 46
        }

        [Test]
        public void Can_get_guard_asleep_most_frequently_on_same_minute()
        {
            var timeline = new GuardEventTimeline(GetEventStringsOutOfOrder());
            var guard = timeline.GetGuardWithMostMinutesAsleepForOneTime();
            var mostMinutesAsleep = guard.Sleeping[guard.TimeTheySleptTheMost()];
            var timeSleepingMost = guard.Sleeping.Single(x => x.Value == mostMinutesAsleep);
            Assert.That(99, Is.EqualTo(guard.GuardId));
            Assert.That(45, Is.EqualTo(timeSleepingMost.Key.Minute));
        }

        [Test]
        public void Can_get_Day_4_part_2_answer()
        {
            var timeline = new GuardEventTimeline(TestDataProvider.GetPuzzleInput());
            var guard = timeline.GetGuardWithMostMinutesAsleepForOneTime();
            var mostMinutesAsleep = guard.Sleeping[guard.TimeTheySleptTheMost()];
            var timeSleepingMost = guard.Sleeping.Single(x => x.Value == mostMinutesAsleep);
            Assert.That(373, Is.EqualTo(guard.GuardId));
            Assert.That(40, Is.EqualTo(timeSleepingMost.Key.Minute));
            //Puzzle answer is 373 * 40
        }

        private string[] GetEventStringsInOrder()
        {
            string[] eventStrings = new string[]
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up"
            };
            return eventStrings;
        }

        private string[] GetEventStringsOutOfOrder()
        {
            string[] eventStrings = new string[]
            {
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-05 00:55] wakes up",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
            };
            return eventStrings;
        }
    }
}
