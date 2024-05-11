using Dynamo.Business.Shared.Casino.Slots;
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
                player.Bets = new List<Bet>();
                if (spaceToBetOn == SpaceType.AllForOne)
                    player.Bets.Add(new AllForOneBet(10));
                else
                    player.Bets.Add(new CalledShotBet(10, spaceToBetOn));
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

        public void PlayGame(int wheelSpeedAverage, SpaceType initialSpace)
        {
            //Pot = 0;
            PlaceBets();


            double lambda = (double)1 / wheelSpeedAverage; //Correspnds to mean of 1/lambda = 10;
            var wheelSpeedExponential = new Exponential(lambda);
            var wheelSpeed = (int)Math.Floor(wheelSpeedExponential.Sample());

            //var wheelSpeed = wheelSpeedAverage;


            var winningSpace = Wheel.Spin(initialSpace, wheelSpeed);
            foreach (var player in Players)
            {
                foreach (var bet in player.Bets)
                {
                    Pot += bet.Amount;
                    player.Amount -= bet.Amount;
                    var payout = bet.GetPayout(winningSpace.Value);
                    player.Amount += payout;
                    Pot -= payout;
                }
            }
        }
    }
}
