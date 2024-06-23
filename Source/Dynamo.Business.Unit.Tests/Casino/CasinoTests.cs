using System.Collections.Generic;
using System.Linq;
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
            var timeIncrement = 0.0021739130;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player, timeIncrement);
                playerNum++;

                if (casino.CurrentTime > 12)
                {
                    var numberOfUsers = casino.Players.Count;
                    numbersOfUsers.Add(numberOfUsers);
                }
            }

            var averageNumberOfUsers = numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            //Accepted answer was 927
            Assert.That(averageNumberOfUsers, Is.GreaterThan(900));
            Assert.That(averageNumberOfUsers, Is.LessThan(950));
        }

        [Test]
        public void Can_simulate_arrivals_using_poisson_process()
        {
            var casino = new Shared.Casino.Casino();
            int n = 11040; //This is 460 per hour for 24 hours
            double scale = 2;

            double[] timeInCasino = MathHelper.GenerateExponentialRandomVariables(scale, n);
            double[] arrivalTimes = MathHelper.GenerateExponentialRandomVariables(7.826 / 60 / 60 / 24, n); //7.826 is in seconds

            casino.TotalTime = 24;

            var numbersOfUsers = new List<int>();

            var playerNum = 0;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player, arrivalTimes[playerNum]);
                playerNum++;

                var numberOfUsers = casino.Players.Count;
                numbersOfUsers.Add(numberOfUsers);
            }

            var averageNumberOfUsers = (double)numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            //Accepted answer was 927
            Assert.That(averageNumberOfUsers, Is.GreaterThan(900));
            Assert.That(averageNumberOfUsers, Is.LessThan(955));
        }

        [Test, Ignore("Taks too long")]
        public void Can_simulate_for_three_years()
        {
            var daysToRunSimulation = 1095;
            double totalUsers = 0;
            for (var day = 0; day < daysToRunSimulation; day++)
            {
                totalUsers += SimulateFor24Hours();
            }

            var averageUsers = totalUsers / daysToRunSimulation;
            Assert.That(averageUsers, Is.GreaterThan(900));
            Assert.That(averageUsers, Is.LessThan(934));
        }

        [Test, Ignore("Taks too long")]
        public void Can_simulate_for_3_years()
        {
            var averageUsers = SimulateFor3Years();
            //919 was accepted
            Assert.That(averageUsers, Is.GreaterThan(918));
            Assert.That(averageUsers, Is.LessThan(920));
        }

        private double SimulateFor3Years()
        {
            var casino = new Shared.Casino.Casino();
            int n = 11040 * 1095; //This is 460 per hour for 24 hours
            double scale = 2;

            double[] timeInCasino = MathHelper.GenerateExponentialRandomVariables(scale, n);
            double[] arrivalTimes = MathHelper.GenerateExponentialRandomVariables(0.0021739130, n); //7.826 is in seconds

            foreach (var arrivalTime in arrivalTimes)
            {
                var x = arrivalTime;
            }

            casino.TotalTime = 24 * 1095;

            var numbersOfUsers = new List<long>();

            var playerNum = 0;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player, arrivalTimes[playerNum]);
                playerNum++;

                var numberOfUsers = casino.Players.Count;
                numbersOfUsers.Add(numberOfUsers);
            }

            var averageNumberOfUsers = (double)numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            return averageNumberOfUsers;
        }

        private double SimulateFor24Hours()
        {
            var casino = new Shared.Casino.Casino();
            int n = 11040; //This is 460 per hour for 24 hours
            double scale = 2;

            double[] timeInCasino = MathHelper.GenerateExponentialRandomVariables(scale, n);
            double[] arrivalTimes = MathHelper.GenerateExponentialRandomVariables(0.0021739130, n); //7.826 is in seconds

            foreach (var arrivalTime in arrivalTimes)
            {
                var x = arrivalTime;
            }

            casino.TotalTime = 24;

            var numbersOfUsers = new List<int>();

            var playerNum = 0;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player, arrivalTimes[playerNum]);
                playerNum++;

                var numberOfUsers = casino.Players.Count;
                numbersOfUsers.Add(numberOfUsers);
            }

            var averageNumberOfUsers = (double)numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            return averageNumberOfUsers;
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
            Assert.That(averageTimeInGame, Is.LessThan(0.398));
        }

        [Test]
        public void Can_create_a_game_room_and_play_all_three_games()
        {
            var playerCount = 500000;
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
            gameRoom.MovePlayersThroughTheGame();

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
            Assert.That(averageTimeInGame, Is.GreaterThan(1.0));
            Assert.That(averageTimeInGame, Is.LessThan(1.13));
        }

    }
}
