using Dynamo.Business.Shared.Casino.Roulette;
using MathNet.Numerics.Distributions;
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
            var random = new Random();
            var numberOfPlayers = 6;
            var game = new Game(numberOfPlayers);
            foreach (var player in game.Players)
            {
                Assert.That(player.Bets.Count, Is.EqualTo(0));
            }
            var spaceToBetOn = (SpaceType)random.Next(0, 11);
            game.PlaceBets();
            foreach (var player in game.Players)
            {
                Assert.That(player.Bets.Count, Is.AtLeast(1));
            }
        }

        [Test]
        public void Play_game()
        {
            var game = new Game(numberOfPlayers: 6);
            game.PlayGame(5, SpaceType.Five);
            foreach (var player in game.Players)
            {
                Assert.That(player.Amount, Is.Not.EqualTo(0));
            }
        }

        [Test]
        public void Solve_spring_2024_week_5_part_1()
        {
            var random = new Random();
            var results = new List<Tuple<int, int, SpaceType>>();
            foreach (SpaceType spaceType in Enum.GetValues(typeof(SpaceType)))
            {
                for (var wheelSpeedAverage = 1; wheelSpeedAverage < 11; wheelSpeedAverage++)
                {
                    var game = new Game(numberOfPlayers: 6);
                    //var gamesPerHour = (int)Math.Floor(Normal.Sample(random, 50, 1));
                    var gamesPerHour = 5000;
                    for (var i = 0; i < gamesPerHour; i++)
                    {
                        game.PlayGame(wheelSpeedAverage, spaceType);
                    }
                    results.Add(new Tuple<int, int, SpaceType>(game.Pot, wheelSpeedAverage, spaceType));
                }
            }
            var x = results;
            var maxPot = results.Max(x => x.Item1);
            var maxResult = results.First(x => x.Item1 == maxPot);

            //These are the accepted answers for 
            Assert.That(maxResult.Item2, Is.EqualTo(1));
            Assert.That(maxResult.Item3, Is.EqualTo(SpaceType.AllForOne));
        }

        [Test]
        public void Play_with_normal_distributions()
        {
            var random = new Random();
            var total = 0.0;
            for (var i = 0; i < 10000; i++)
            {
                double numberOfGamesPlayed = Math.Floor(Normal.Sample(random, 10, 1));
                double lambda = 0.1; //Correspnds to mean of 1/lambda = 10;
                var wheelSpeedExponential = new Exponential(lambda);
                var wheelSpeed = wheelSpeedExponential.Sample();
                total += wheelSpeed;
            }

            var average = (double)total / 10000;

        }

        [Test]
        public void Can_spin_wheel()
        {
            var wheel = new Wheel();
            var startingSpace = SpaceType.Six;
            var winningSpace = wheel.Spin(startingSpace, 4);
            Assert.That(winningSpace.Value, Is.EqualTo(SpaceType.Ten));
            winningSpace = wheel.Spin(startingSpace, 5);
            Assert.That(winningSpace.Value, Is.EqualTo(SpaceType.AllForOne));
            winningSpace = wheel.Spin(startingSpace, 11);
            Assert.That(winningSpace.Value, Is.EqualTo(SpaceType.Six));
            winningSpace = wheel.Spin(startingSpace, 16);
            Assert.That(winningSpace.Value, Is.EqualTo(SpaceType.AllForOne));
        }
    }
}
