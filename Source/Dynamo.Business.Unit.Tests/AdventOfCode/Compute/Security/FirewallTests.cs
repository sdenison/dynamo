using Dynamo.Business.Shared.AdventOfCode.Compute.Security;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Security
{
    [TestFixture]
    public class FirewallTests
    {
        [Test]
        public void Can_create_firewall_with_layers()
        {
            string[] layerStrings = new string[]
            {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"
            };
            var fireWall = new Firewall(layerStrings);
            Assert.That(fireWall.Layers.Count, Is.EqualTo(7));
            Assert.That(fireWall.Layers.All(x => x.SecurityScanDepth == 1));
            // Picosecond 0
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.Layers[0].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[1].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[4].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[6].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.CaughtCount, Is.EqualTo(1));
            // Picosecond 1
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.Layers[0].SecurityScanDepth, Is.EqualTo(3));
            Assert.That(fireWall.Layers[1].SecurityScanDepth, Is.EqualTo(1));
            Assert.That(fireWall.Layers[4].SecurityScanDepth, Is.EqualTo(3));
            Assert.That(fireWall.Layers[6].SecurityScanDepth, Is.EqualTo(3));
            Assert.That(fireWall.CaughtCount, Is.EqualTo(1));
            // Picosecond 2
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.Layers[0].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[1].SecurityScanDepth, Is.EqualTo(2));
            Assert.That(fireWall.Layers[4].SecurityScanDepth, Is.EqualTo(4));
            Assert.That(fireWall.Layers[6].SecurityScanDepth, Is.EqualTo(4));
            Assert.That(fireWall.CaughtCount, Is.EqualTo(1));
            // Picosecond 3
            fireWall.AdvanceOnePicosecond();
            // Picosecond 4
            fireWall.AdvanceOnePicosecond();
            // Picosecond 5
            fireWall.AdvanceOnePicosecond();
            // Picosecond 6
            fireWall.AdvanceOnePicosecond();
            Assert.That(fireWall.CaughtCount, Is.EqualTo(2));
            Assert.That(fireWall.SeverityCount, Is.EqualTo(24));
        }

        [Test]
        public void Can_get_2017_day_13_part_1_answer()
        {
            var layerStrings = GetPuzzleData();
            var fireWall = new Firewall(layerStrings);
            fireWall.AdvanceAllPicoseconds();
            Assert.That(fireWall.SeverityCount, Is.EqualTo(632));
        }

        [Test]
        public void Can_get_correct_for_clean_run()
        {
            string[] layerStrings = new string[]
            {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"
            };
            var fireWall = new Firewall(layerStrings);
            var delayWorks = fireWall.AdvanceAllPicosecondsWithDelay(10);
            Assert.That(delayWorks, Is.EqualTo(true));
            fireWall = new Firewall(layerStrings);
            delayWorks = fireWall.AdvanceAllPicosecondsWithDelay(11);
            Assert.That(delayWorks, Is.EqualTo(false));
        }

        [Test]
        public void Can_get_2017_day_13_part_2_answer()
        {
            var delay = 0;
            var layerStrings = GetPuzzleData();
            for (var i = 9999; i < 10000000; i++)
            {
                var fireWall = new Firewall(layerStrings);
                if (fireWall.AdvanceAllPicosecondsWithDelay(i))
                {
                    delay = i;
                    break;
                }
            }
            Assert.That(delay, Is.EqualTo(33));
        }


        public string[] GetPuzzleData()
        {
            return new string[] {
                "0: 3",
                "1: 2",
                "2: 6",
                "4: 4",
                "6: 4",
                "8: 8",
                "10: 9",
                "12: 8",
                "14: 5",
                "16: 6",
                "18: 8",
                "20: 6",
                "22: 12",
                "24: 6",
                "26: 12",
                "28: 8",
                "30: 8",
                "32: 10",
                "34: 12",
                "36: 12",
                "38: 8",
                "40: 12",
                "42: 12",
                "44: 14",
                "46: 12",
                "48: 14",
                "50: 12",
                "52: 12",
                "54: 12",
                "56: 10",
                "58: 14",
                "60: 14",
                "62: 14",
                "64: 14",
                "66: 17",
                "68: 14",
                "72: 14",
                "76: 14",
                "80: 14",
                "82: 14",
                "88: 18",
                "92: 14",
                "98: 18"
            };
        }
    }
}
