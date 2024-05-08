using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Game
    {
        public List<Player> Players { get; private set; }

        public Game(int numberOfPlayers)
        {
            Players = new List<Player>();
            for (var i = 0; i < 6; i++)
                Players.Add(new Player());
        }

        public void PlaceBets()
        {
            var random = new Random();
            foreach (var player in Players)
            {
                var spaceToBetOn = (SpaceType)random.Next(0, 11);
                var betType = spaceToBetOn == SpaceType.CalledShot ? BetType.CalledShot : BetType.AllForOne;
                player.Bets.Add(new Bet(amount: 10, spaceToBetOn, mainBet: true));
            }
            foreach (var player in Players)
            {
                foreach (var playerToBetAgainst in Players)
                {
                    if (player == playerToBetAgainst)
                        continue;
                    var takeBet = random.Next(0, 2) == 1;
                    if (takeBet)
                    {
                        Bet playerBet;
                        if (player.Bets.Any(x => x.Space == playerToBetAgainst.Bets[0].Space))
                            playerBet = player.Bets.First(x => x.Space == playerToBetAgainst.Bets[0].Space);
                        else
                        {
                            playerBet = new Bet(1, playerToBetAgainst.Bets[0].Space, mainBet: false);
                            player.Bets.Add(playerBet);
                        }
                        playerBet.Amount += 1;
                    }
                }
            }

        }
    }
}
