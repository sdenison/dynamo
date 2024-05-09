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
