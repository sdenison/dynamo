using System.Diagnostics;
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
            var highScore = game.HighScore;
            Assert.AreEqual(32, highScore);
        }

        [TestCase("10 players; last marble is worth 1618", 8317)]
        [TestCase("13 players; last marble is worth 7999", 146373)]
        [TestCase("17 players; last marble is worth 1104", 2764)]
        [TestCase("21 players; last marble is worth 6111", 54718)]
        [TestCase("30 players; last marble is worth 5807", 37305)]
        public void Can_play_marble_game2(string gameDescription, int highScore)
        {
            var game = new Game(gameDescription);
            game.PlayGame();
            Assert.AreEqual(highScore, game.HighScore);
        }

        [Test]
        public void Can_get_day_9_part_1_answer()
        {
            var game = new Game("426 players; last marble is worth 72058 points");
            game.PlayGame();
            Assert.AreEqual(424112, game.HighScore);
        }

        [Test, Ignore("Takes too long")]
        public void Can_get_day_9_part_2_answer()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var game = new Game("426 players; last marble is worth 7205800 points");
            game.PlayGame();
            stopWatch.Stop();
            var numberOfSeconds = stopWatch.ElapsedMilliseconds / 1000;
            Assert.AreEqual(3487352628, game.HighScore);
        }
    }
}
