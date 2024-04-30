using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.StreetDice
{
    public class Game
    {
        public List<Player> Players { get; private set; }

        public Game(List<Player> players)
        {
            Players = players;
        }

        public static Game Parse(List<string> playerStrings)
        {
            var players = new List<Player>();
            foreach (var playerString in playerStrings)
            {
                players.Add(Player.Parse(playerString));
            }
            foreach (var player in players)
            {
                player.LoadNemesises(players);
            }
            return new Game(players);
        }

        public List<Bet> GetBets(Player shooter)
        {
            int shooterBet = (int)Math.Ceiling(shooter.CurrentMoney * shooter.MaximumPercentage / 2);
            //Shooter always bets
            var bets = new List<Bet> { new Bet(toWin: true, amount: shooterBet, player: shooter) };

            foreach (var player in Players)
            {
                if (player == shooter)
                    continue;
                if (player.Nemeses.Contains(shooter))
                {
                    int nemesisBet = 0;

                    if (shooterBet > player.CurrentMoney)
                        //Go all in
                        nemesisBet = player.CurrentMoney;
                    else if (shooterBet > player.MaxComfortableBet)
                        //Take the shooter's number
                        nemesisBet = shooterBet;
                    else
                        //Use player's number
                        nemesisBet = player.MaxComfortableBet;

                    if (nemesisBet == 0)
                        throw new Exception();

                    if (nemesisBet > player.CurrentMoney)
                        throw new Exception();

                    bets.Add(new Bet(toWin: false, nemesisBet, player));
                }
                else
                {
                    if (player.MaxComfortableBet >= shooterBet)
                    {
                        bets.Add(new Bet(toWin: true, shooterBet, player));
                    }
                }
            }
            return bets;
        }
    }

    public class Bet
    {
        public bool ToWin { get; private set; }
        public int Amount { get; private set; }
        public Player Player { get; private set; }

        public Bet(bool toWin, int amount, Player player)
        {
            ToWin = toWin;
            Amount = amount;
            Player = player;
        }
    }
}
