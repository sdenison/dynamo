using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public Wheel Wheel { get; private set; }
        public int Pot { get; private set; }

        public Game(int numberOfPlayers)
        {
            Players = new List<Player>();
            for (var i = 0; i < 6; i++)
                Players.Add(new Player());
            Wheel = new Wheel();
        }

        public void PlaceBets()
        {
            var random = new Random();
            foreach (var player in Players)
            {
                var spaceToBetOn = (SpaceType)random.Next(0, 11);
                if (spaceToBetOn == SpaceType.AllForOne)
                    player.Bets.Add(new AllForOneBet(10));
                else
                    player.Bets.Add(new CalledShotBet(10, spaceToBetOn));
                {
                    var addOddsBet = random.Next(0, 2) == 1;
                    if (addOddsBet)
                        player.Bets.Add(new OddsBet(1));
                    var addEvensBet = random.Next(0, 2) == 1;
                    if (addEvensBet)
                        player.Bets.Add(new EvensBet(1));
                    var addHandBet = random.Next(0, 2) == 1;
                    if (addHandBet)
                        player.Bets.Add(new HandBet(1));
                    var addBushBet = random.Next(0, 2) == 1;
                    if (addBushBet)
                        player.Bets.Add(new BushBet(1));
                }
            }
        }

        public void PlayGame(int wheelSpeedAverage)
        {
            Pot = 0;
            //var wheelSpeed = MathHelper.GenerateExponentialRandomVariables(1, wheelSpeedAverage);
            double lambda = 0.1; //Correspnds to mean of 1/lambda = 10;
            var wheelSpeed = new Exponential(lambda);

        }
    }
}
