using Dynamo.Business.Shared.Casino.Roulette;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Business.Unit.Tests.Casino.Roulette
{
    public class GameTests
    {
        [Test]
        public void Can_create_roulette_game()
        {
            var numberOfPlayers = 6;
            var game = new Game(numberOfPlayers);
            Assert.That(game.Players.Count, Is.EqualTo(numberOfPlayers));
        }

        [Test]
        public void Players_can_place_bets()
        {
            var numberOfPlayers = 6;
            var game = new Game(numberOfPlayers);
            foreach (var player in game.Players)
            {
                Assert.That(player.Bets.Count, Is.EqualTo(0));
            }
            game.PlaceBets();
            foreach (var player in game.Players)
            {
                Assert.That(player.Bets.Count, Is.AtLeast(1));
            }
        }

    }
}
