using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.AdventOfCode.Marbles;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Marbles
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Can_create_a_marble_game()
        {
            var gameDescription = "9 players; last marble is worth 25 points";
            var game = new Game(gameDescription);
            Assert.AreEqual(9, game.NumberOfPlayers);
            Assert.AreEqual(25, game.LastMarbleValue);
        }

        [Test]
        public void Can_play_marble_game()
        {
            var gameDescription = "9 players; last marble is worth 25 points";
            var game = new Game(gameDescription);
            game.PlayGame();
            var highScore = game.Players.OrderByDescending(x => x.Score).First().Score;
            Assert.AreEqual(32, highScore);
        }

    }
}
