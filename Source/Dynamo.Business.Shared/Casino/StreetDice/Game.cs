using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Casino.StreetDice
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public SortedDictionary<int, Player> PlayersInGame { get; private set; }
        public int Pot { get; private set; }
        public int RoundsPlayed { get; set; }

        public Game(List<Player> players)
        {
            Players = players;
            Pot = 0;
            RoundsPlayed = 0;
        }

        public static Game Parse(List<string> playerStrings)
        {
            var players = new List<Player>();
            foreach (var playerString in playerStrings)
            {
                players.Add(Player.Parse(playerString));
            }

            for (int i = 1; i < players.Count - 1; i++)
            {
                players[i].NextPlayer = players[i + 1];
                players[i].PreviousPlayer = players[i - 1];
            }
            players[0].PreviousPlayer = players[players.Count - 1];
            players[0].NextPlayer = players[1];
            players[players.Count - 1].NextPlayer = players[0];
            players[players.Count - 1].PreviousPlayer = players[players.Count - 2];

            foreach (var player in players)
            {
                player.LoadNemesises(players);
            }
            return new Game(players);
        }

        public static List<bool> ParseRollOutcomes(string rollOutcomesString)
        {
            var outcomes = new List<bool>();
            foreach (var outcomeString in rollOutcomesString.Split(","))
            {
                if (outcomeString == "W")
                    outcomes.Add(true);
                else
                    outcomes.Add(false);
            }
            return outcomes;
        }

        public List<Bet> GetBets(Player shooter)
        {
            shooter.SatOutInARow = 0;
            int shooterBet = (int)Math.Ceiling(shooter.CurrentMoney * shooter.MaximumPercentage / 2);

            //Shooter always bets
            var bets = new List<Bet> { new Bet(toWin: true, amount: shooterBet, player: shooter) };
            var player = shooter.NextPlayer;

            while (shooter != player)
            {
                if (player.Nemeses.Contains(shooter))
                {
                    player.SatOutInARow = 0;
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
                        throw new Exception($"Player {player.PlayerId} has nemisisBet of 0 when shooter is {shooter.PlayerId}");

                    if (nemesisBet > player.CurrentMoney)
                        throw new Exception();

                    bets.Add(new Bet(toWin: false, nemesisBet, player));
                }
                else
                {
                    if (player.MaxComfortableBet >= shooterBet)
                    {
                        bets.Add(new Bet(toWin: true, shooterBet, player));
                        player.SatOutInARow = 0;
                    }
                    else
                    {
                        player.SatOutInARow += 1;
                    }
                }
                player = player.NextPlayer;
            }
            return bets;
        }

        public void PlayRound(Player shooter, bool win)
        {

            var totalPlayerMoney = TotalPlayerMoney(shooter) + Pot;

            RoundsPlayed++;
            var bets = GetBets(shooter);

            foreach (var bet in bets)
            {
                bet.Player.CurrentMoney -= bet.Amount;
                Pot += bet.Amount;
            }

            var winners = bets.Where(x => x.ToWin == win).ToList();
            if (winners.Count > 0)
            {
                var winAmount = (int)Pot / winners.Count;
                foreach (var bet in winners)
                {
                    bet.Player.CurrentMoney += winAmount;
                    Pot -= winAmount;
                }
            }
        }

        public void PlayGame(List<bool> rollOutcomes)
        {
            RoundsPlayed = 0;
            var shooter = Players[0];
            //var player = Players[0];
            foreach (var rollOutcome in rollOutcomes)
            {
                if (shooter.NextPlayer == shooter)
                {
                    shooter.CurrentMoney += Pot;
                    Pot = 0;
                    return;
                }
                PlayRound(shooter, rollOutcome);
                var player = shooter.NextPlayer;
                while (player != shooter)
                {
                    if (player.SatOutInARow >= 3 || player.CurrentMoney == 0 || AllNemesisHaveDropped(player))
                    {
                        RemovePlayer(player);
                    }
                    player = player.NextPlayer;
                }
                if (shooter.SatOutInARow >= 3 || shooter.CurrentMoney == 0 || AllNemesisHaveDropped(shooter))
                {
                    RemovePlayer(shooter);
                }
                shooter = shooter.NextPlayer;
            }
        }

        private void RemovePlayer(Player player)
        {
            player.PreviousPlayer.NextPlayer = player.NextPlayer;
            player.NextPlayer.PreviousPlayer = player.PreviousPlayer;
        }

        public bool AllNemesisHaveDropped(Player player)
        {
            var currentPlayer = player;
            while (currentPlayer.NextPlayer != player)
            {
                if (currentPlayer.Nemeses.Contains(player))
                    return false;
                currentPlayer = currentPlayer.NextPlayer;
            }
            return true;
        }

        public int TotalPlayerMoney(Player shooter)
        {
            var totalMoney = shooter.CurrentMoney;
            var player = shooter.NextPlayer;
            while (player != shooter)
            {
                totalMoney += player.CurrentMoney;
                player = player.NextPlayer;
            }
            return totalMoney;
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
