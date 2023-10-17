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
