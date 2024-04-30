using Dynamo.Business.Shared.Casino.StreetDice;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.Casino.StreetDice
{
    public class GameTests
    {
        [Test]
        public void Let_player_1_play_the_game()
        {
            var playerStrings = new List<string>
            {
                "1: $40 50% - 4,3",
                "2: $50 40% - 4,1",
                "3: $10 30% - 2,1",
                "4: $20 35% - 2,3"
            };
            var game = Game.Parse(playerStrings);
            double largestPotSize = 0;
            Player winningPlayer = null;
            var bets = game.GetBets(game.Players[0]);
            Assert.That(bets.Count, Is.EqualTo(3));
            Assert.That(bets[0].Amount, Is.EqualTo(10));
            Assert.That(bets[1].Amount, Is.EqualTo(20));
            Assert.That(bets[2].Amount, Is.EqualTo(10));
            Assert.That(bets.Sum(x => x.Amount), Is.EqualTo(40));
        }

        [Test]
        public void Can_play_game_from_spring_2024_week_4_part_1_example()
        {
            var playerStrings = new List<string>

            {
                "1: $40 50% - 4,3",
                "2: $50 40% - 4,1",
                "3: $10 30% - 2,1",
                "4: $20 35% - 2,3"
            };
            var game = Game.Parse(playerStrings);
            double largestPotSize = 0;
            Player winningPlayer = null;
            foreach (var shooter in game.Players)
            {
                var bets = game.GetBets(shooter);
                if (largestPotSize < bets.Sum(x => x.Amount))
                {
                    largestPotSize = bets.Sum(x => x.Amount);
                    winningPlayer = shooter;
                }
            }

            Assert.That(largestPotSize, Is.EqualTo(44));
        }

        [Test]
        public void Can_get_2024_week_4_part_1_answer()
        {
            var playerStrings = TestDataProvider.GetPlayerStrings();
            var game = Game.Parse(playerStrings);
            int largestPotSize = 0;
            Player winningPlayer = null;
            foreach (var shooter in game.Players)
            {
                var bets = game.GetBets(shooter);
                if (largestPotSize < bets.Sum(x => x.Amount))
                {
                    largestPotSize = bets.Sum(x => x.Amount);
                    winningPlayer = shooter;
                }
            }

            Assert.That(winningPlayer.PlayerId, Is.EqualTo(424));
            Assert.That(largestPotSize, Is.EqualTo(2320));
        }

        [Test]
        public void Can_load_game_from_big_list_of_players()
        {
            var playerStrings = TestDataProvider.GetPlayerStrings();
            var game = Game.Parse(playerStrings);

            Assert.That(game.Players.Count, Is.EqualTo(500));
            //Spot Checking that players loaded correctly
            Assert.That(game.Players[50].PlayerId, Is.EqualTo(51));
            Assert.That(game.Players[50].Nemeses[3].PlayerId, Is.EqualTo(102));
            Assert.That(game.Players[499].PlayerId, Is.EqualTo(500));
            Assert.That(game.Players[499].Nemeses.Last().PlayerId, Is.EqualTo(433));
        }
    }
}
