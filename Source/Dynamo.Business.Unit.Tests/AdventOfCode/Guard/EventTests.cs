using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

            Assert.AreEqual(1518, guardEvent.Year);
            Assert.AreEqual(11, guardEvent.Month);
            Assert.AreEqual(1, guardEvent.Day);
            Assert.AreEqual(0, guardEvent.Hour);
            Assert.AreEqual(0, guardEvent.Minute);
            Assert.AreEqual(EventType.BeginShift, guardEvent.EventType);
            Assert.AreEqual(10, guardEvent.GuardId);
        }

        [Test]
        public void Can_get_timeline()
        {
            var timeLine = new GuardEventTimeline(GetEventStringsInOrder());
            Assert.AreEqual(17, timeLine.GuardEvents.Count);
        }

        [Test]
        public void Can_get_timeline_out_of_order()
        {
            var timeLine = new GuardEventTimeline(GetEventStringsOutOfOrder());
            Assert.AreEqual(17, timeLine.GuardEvents.Count);
            Assert.AreEqual(0, timeLine.GuardEvents.ToList()[0].Hour);
            Assert.AreEqual(0, timeLine.GuardEvents.ToList()[0].Minute);
            Assert.AreEqual(0, timeLine.GuardEvents.ToList()[16].Hour);
            Assert.AreEqual(55, timeLine.GuardEvents.ToList()[16].Minute);
        }

        [Test]
        public void Can_create_date_in_1500s()
        {
            var datetime = new DateTime(1518, 6, 16);
            Assert.AreEqual(1518, datetime.Year);
        }

        [Test]
        public void Can_get_example_answer()
        {
            var timeline = new GuardEventTimeline(GetEventStringsOutOfOrder());
            var guardThatSleptTheMost = timeline.GetGuardWithMostMinutesAsleep();
            Assert.AreEqual(10, guardThatSleptTheMost.GuardId);
            var timeTheySleptTheMost = guardThatSleptTheMost.TimeTheySleptTheMost();
            Assert.AreEqual(24, timeTheySleptTheMost.Minute);
        }

        [Test]
        public void Can_get_Day_4_part_1_answer()
        {
            var timeline = new GuardEventTimeline(TestDataProvider.GetPuzzleInput());
            var guardThatSleptTheMost = timeline.GetGuardWithMostMinutesAsleep();
            Assert.AreEqual(863, guardThatSleptTheMost.GuardId);
            var timeTheySleptTheMost = guardThatSleptTheMost.TimeTheySleptTheMost();
            Assert.AreEqual(46, timeTheySleptTheMost.Minute);
            //Puzzle answerr is 863 * 46
        }

        [Test]
        public void Can_get_guard_asleep_most_frequently_on_same_minute()
        {
            var timeline = new GuardEventTimeline(GetEventStringsOutOfOrder());
            var guard = timeline.GetGuardWithMostMinutesAsleepForOneTime();
            var mostMinutesAsleep = guard.Sleeping[guard.TimeTheySleptTheMost()];
            var timeSleepingMost = guard.Sleeping.Single(x => x.Value == mostMinutesAsleep);
            Assert.AreEqual(99, guard.GuardId);
            Assert.AreEqual(45, timeSleepingMost.Key.Minute);
        }

        [Test]
        public void Can_get_Day_4_part_2_answer()
        {
            var timeline = new GuardEventTimeline(TestDataProvider.GetPuzzleInput());
            var guard = timeline.GetGuardWithMostMinutesAsleepForOneTime();
            var mostMinutesAsleep = guard.Sleeping[guard.TimeTheySleptTheMost()];
            var timeSleepingMost = guard.Sleeping.Single(x => x.Value == mostMinutesAsleep);
            Assert.AreEqual(373, guard.GuardId);
            Assert.AreEqual(40, timeSleepingMost.Key.Minute);
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
