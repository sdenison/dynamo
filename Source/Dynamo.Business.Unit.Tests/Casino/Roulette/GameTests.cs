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
                    var gamesPerHour = 100;
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

            //These are the accepted answers for part 1
            Assert.That(maxResult.Item2, Is.EqualTo(1));
            Assert.That(maxResult.Item3, Is.EqualTo(SpaceType.AllForOne));
        }

        [Test]
        public void Solve_spring_2024_week_5_part_2()
        {
            var decelerationCoef = 0.5f;
            var results = new List<Tuple<int, float, SpaceType>>();
            while (decelerationCoef < 1)
            {
                foreach (SpaceType ballDropSpace in Enum.GetValues(typeof(SpaceType)))
                {
                    var game = new Game(numberOfPlayers: 6);
                    var gamesPerHour = 100;
                    for (var i = 0; i < gamesPerHour; i++)
                    {
                        game.PlayGameComplex(ballDropSpace, decelerationCoef);
                    }
                    results.Add(new Tuple<int, float, SpaceType>(game.Pot, decelerationCoef, ballDropSpace));
                }
                decelerationCoef += 0.01f;
            }

            var maxPot = results.Max(x => x.Item1);
            var maxResult = results.First(x => x.Item1 == maxPot);

            //These are the accepted answers for part 2
            Assert.That(maxResult.Item3, Is.EqualTo(SpaceType.Nine));
            //Assert.That(maxResult.Item2, Is.EqualTo(.72));
        }


        [Test]
        public void Can_spin_wheel()
        {
            var wheel = new Wheel();
            var startingSpace = SpaceType.Six;
            var winningSpace = wheel.SpinWheel(startingSpace, 4);
            Assert.That(winningSpace, Is.EqualTo(SpaceType.Ten));
            winningSpace = wheel.SpinWheel(startingSpace, 5);
            Assert.That(winningSpace, Is.EqualTo(SpaceType.AllForOne));
            winningSpace = wheel.SpinWheel(startingSpace, 11);
            Assert.That(winningSpace, Is.EqualTo(SpaceType.Six));
            winningSpace = wheel.SpinWheel(startingSpace, 16);
            Assert.That(winningSpace, Is.EqualTo(SpaceType.AllForOne));
        }
    }
}
