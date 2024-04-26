using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.Casino;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Casino
{
    public class CasinoTests
    {
        [Test]
        public void Can_find_average_number_of_players_for_the_last_12_hours()
        {
            var casino = new Shared.Casino.Casino();
            int n = 11040; //This is 460 per hour for 24 hours
            double scale = 2; //This is the average of how long they spend in the casino

            double[] timeInCasino = MathHelper.GenerateExponentialRandomVariables(scale, n);

            casino.TotalTime = 24;

            var numbersOfUsers = new List<int>();

            var playerNum = 0;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player);
                playerNum++;

                if (casino.CurrentTime > 12)
                {
                    var numberOfUsers = casino.Players.Count;
                    numbersOfUsers.Add(numberOfUsers);
                }
            }

            var averageNumberOfUsers = numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            //Accepted answer was 927
            Assert.That(averageNumberOfUsers, Is.GreaterThan(908));
            Assert.That(averageNumberOfUsers, Is.LessThan(934));
        }

        [Test]
        public void Can_create_a_blackjack_game()
        {
            var playerCount = 500000;
            var blackjackGame = new Game(playerCount, 0.1666667); //1.66666667 is 1/10th of an hour or 10 minutes

            var players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                players.Add(new Player());
            }

            double currentTime = 0;
            foreach (var player in players)
            {
                currentTime = blackjackGame.AddPlayer(player, currentTime);
                currentTime += 0.21739562;
            }

            double totalTime = 0;
            double totalGameTime = 0;
            foreach (var player in players)
            {
                var gameTime = player.GameTime;
                var waitTime = player.WaitTime;
                totalTime += gameTime + waitTime;
                totalGameTime += player.TotalGameTime;
            }

            var averageTotalGameTime = totalGameTime / playerCount;
            Assert.That(averageTotalGameTime, Is.GreaterThan(0.15));
            Assert.That(averageTotalGameTime, Is.LessThan(0.17)); //Should converge to 0.16666667


            var averageTimeInGame = totalTime / playerCount;
            //These numbers seem reasonable for a game that lasts 10 minutes on average
            Assert.That(averageTimeInGame, Is.GreaterThan(0.36));
            Assert.That(averageTimeInGame, Is.LessThan(0.39));
        }

        [Test]
        public void Can_create_a_game_room_and_play_all_three_games()
        {
            var playerCount = 5000000;
            var gameRoom = new GameRoom(playerCount);

            var players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                players.Add(new Player());
            }

            double currentTime = 0;
            foreach (var player in players)
            {
                gameRoom.AddPlayer(player, currentTime);
                currentTime += 0.21739562;
            }

            double totalTime = 0;
            double totalGameTime = 0;
            foreach (var player in players)
            {
                var gameTime = player.TotalGameTime;
                var waitTime = player.WaitTime;
                totalTime += gameTime + waitTime;
                totalGameTime += player.TotalGameTime;
            }

            var averageTotalGameTime = totalGameTime / playerCount;
            //These numbers seem reasonable for a game that lasts 10 minutes on average
            Assert.That(averageTotalGameTime, Is.GreaterThan(0.44));
            Assert.That(averageTotalGameTime, Is.LessThan(0.459)); //This should converge to 0.458333. 10 + 9 + 8.5 minutes is 0.458333 of an hour.

            var averageTimeInGame = totalTime / playerCount;
            //These numbers seem reasonable for a game that lasts 10 minutes on average
            Assert.That(averageTimeInGame, Is.GreaterThan(0.36));
            Assert.That(averageTimeInGame, Is.LessThan(0.42));
        }

    }
}
