using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Dynamo.Business.Shared.Casino.Slots;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Casino.Slots
{
    public class SlotMachineTests
    {
        private readonly List<Threshold> _game1Thresholds = new List<Threshold>()
        {
            new (10, 3),
            new (7, 6),
            new (5, 9),
            new (1, 12),
            new (0, 70),
        };

        private readonly List<Threshold> _game2Thresholds = new List<Threshold>()
        {
            new (10, 1),
            new (7, 5),
            new (5, 8),
            new (1, 12),
            new (0, 74),
        };

        private readonly List<Threshold> _game3Thresholds = new List<Threshold>()
        {
            new (10, 3),
            new (7, 7),
            new (5, 9),
            new (1, 13),
            new (0, 68),
        };

        [Test]
        public void Can_play_a_slot_machine()
        {
            var slotMachine = new SlotMachine(_game1Thresholds);
            var winAmount = slotMachine.PullHandle();
            Assert.That(winAmount, Is.AnyOf(10, 7, 5, 1, 0));
        }

        [Test]
        public void Get_week_2_part_1_tymc_answer()
        {
            var gamesToPlay = 100000;
            var averageWinAmount = GetAverageAmount(_game1Thresholds, gamesToPlay);

            //Accepted answer is 22.2
            Assert.That(averageWinAmount, Is.GreaterThan(22));
            Assert.That(averageWinAmount, Is.LessThan(22.5));
        }

        [Test]
        public void Which_machine_is_better()
        {
            var gamesToPlay = 100000;
            var averageWinAmount1 = GetAverageAmount(_game1Thresholds, gamesToPlay);
            var averageWinAmount2 = GetAverageAmount(_game2Thresholds, gamesToPlay);
            var averageWinAmount3 = GetAverageAmount(_game3Thresholds, gamesToPlay);

            Assert.That(averageWinAmount1, Is.GreaterThan(averageWinAmount2));
            Assert.That(averageWinAmount3, Is.GreaterThan(averageWinAmount1));
        }

        [Test]
        public void Get_percent_over_for_game_3()
        {
            var percentOver = PercentOver(20, _game3Thresholds, 100000);
            Assert.That(percentOver, Is.GreaterThan(0.59));
            Assert.That(percentOver, Is.LessThan(0.61));
        }

        public double GetAverageAmount(List<Threshold> thresholds, int gamesToPlay)
        {
            long totalWinAmount = 0;
            for (var i = 0; i < gamesToPlay; i++)
            {
                var slotMachine = new SlotMachine(thresholds);
                slotMachine.InsertMoney(10);
                slotMachine.PullHandleNumberOfTimes(50);
                totalWinAmount += slotMachine.Money;
            }

            return (double)totalWinAmount / gamesToPlay;
        }

        public double PercentOver(int amount, List<Threshold> thresholds, int gamesToPlay)
        {
            var overAmount = 0;
            for (var i = 0; i < gamesToPlay; i++)
            {
                var slotMachine = new SlotMachine(thresholds);
                slotMachine.InsertMoney(10);
                slotMachine.PullHandleNumberOfTimes(50);
                if (slotMachine.Money > amount)
                    overAmount++;
            }

            return (double)overAmount / gamesToPlay;
        }
    }
}
