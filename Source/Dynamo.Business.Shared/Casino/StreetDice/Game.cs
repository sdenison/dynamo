using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dynamo.Business.Shared.Casino.StreetDice
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public List<Player> PlayersInGame { get; private set; }
        public int Pot { get; private set; }
        public int RoundsPlayed { get; set; }

        public Game(List<Player> players)
        {
            Players = players;
            Pot = 0;
            RoundsPlayed = 0;
            PlayersInGame = new List<Player>();
            foreach (var player in Players)
                PlayersInGame.Add(player);
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

        public static List<bool> ParseRollOutcomes(string rollOutcomesString)
        {
            var outcomes = new List<bool>();
            foreach (var outcomeString in rollOutcomesString.Split(","))
            {
                if (outcomeString.ToLower() == "w")
                    outcomes.Add(true);
                else
                    outcomes.Add(false);
            }
            return outcomes;
        }

        public List<Bet> GetBets(Player shooter)
        {
            shooter.SatOutInARow = 0;

            //Shooter always bets
            var bets = new List<Bet> { new Bet(toWin: true, amount: shooter.ShooterBet, player: shooter) };

            foreach (var player in PlayersInGame.Where(x => x != shooter))
            {
                AddBetForPlayer(shooter.ShooterBet, player, shooter, bets);
            }
            return bets;
        }

        private void AddBetForPlayer(int shooterBet, Player player, Player shooter, List<Bet> bets)
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
                if (shooterBet <= player.MaxComfortableBet)
                {
                    bets.Add(new Bet(toWin: true, shooterBet, player));
                    player.SatOutInARow = 0;
                }
                else
                {
                    player.SatOutInARow += 1;
                }
            }
        }

        public void PlayRound(Player shooter, bool win)
        {
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
            var shooter = PlayersInGame[0];

            foreach (var rollOutcome in rollOutcomes)
            {
                PlayRound(shooter, rollOutcome);

                foreach (var player in PlayersInGame)
                {
                    if (player.SatOutInARow == 3 || player.CurrentMoney <= 0 || AllNemesisHaveDropped(player))
                    {
                        player.Deleted = true;
                    }
                }
                var deletedPlayers = PlayersInGame.Where(x => x.Deleted).ToList();
                foreach (var player in deletedPlayers)
                {
                    PlayersInGame.Remove(player);
                    if (PlayersInGame.Count == 1)
                    {
                        PlayersInGame[0].CurrentMoney += Pot;
                        Pot = 0;
                        return;
                    }
                }
                shooter = GetNextPlayer(shooter.PlayerId);
                RoundsPlayed++;

                //foreach (var player in PlayersInGame.Where(x => x.PlayerId >= shooter.PlayerId))
                //{
                //    if (player.SatOutInARow == 3 || player.CurrentMoney <= 0 || AllNemesisHaveDropped(player))
                //    {
                //        player.Deleted = true;
                //    }
                //}
                //foreach (var player in PlayersInGame.Where(x => x.PlayerId < shooter.PlayerId))
                //{
                //    if (player.SatOutInARow == 3 || player.CurrentMoney <= 0 || AllNemesisHaveDropped(player))
                //    {
                //        player.Deleted = true;
                //    }
                //}
                //var deletedPlayers = PlayersInGame.Where(x => x.Deleted).ToList();
                //foreach (var player in deletedPlayers)
                //{
                //    PlayersInGame.Remove(player);
                //    if (PlayersInGame.Count == 1)
                //    {
                //        PlayersInGame[0].CurrentMoney += Pot;
                //        Pot = 0;
                //        return;
                //    }
                //}
                //shooter = GetNextPlayer(shooter.PlayerId);
                //RoundsPlayed++;

                //while (true)
                //{
                //    var playerToRemove = PlayersInGame.OrderBy(x => x.PlayerId).FirstOrDefault(x => (x.SatOutInARow >= 3 || x.CurrentMoney <= 0 || AllNemesisHaveDropped(x)));
                //    if (playerToRemove != null)
                //    {
                //        PlayersInGame.Remove(playerToRemove);
                //        if (PlayersInGame.Count == 1)
                //        {
                //            PlayersInGame[0].CurrentMoney += Pot;
                //            Pot = 0;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                //shooter = GetNextPlayer(shooter.PlayerId);

                //while (true)
                //{

                //    var playerToRemove = PlayersInGame.OrderBy(x => x.PlayerId).FirstOrDefault(x => x.PlayerId > shooter.PlayerId && (x.SatOutInARow >= 3 || x.CurrentMoney == 0 || AllNemesisHaveDropped(x)));
                //    if (playerToRemove != null)
                //    {
                //        PlayersInGame.Remove(playerToRemove);
                //        if (PlayersInGame.Count == 1)
                //        {
                //            PlayersInGame[0].CurrentMoney += Pot;
                //            Pot = 0;
                //            return;
                //        }
                //    }
                //    else
                //        break;
                //}

                //while (true)
                //{
                //    var playerToRemove = PlayersInGame.OrderBy(x => x.PlayerId).FirstOrDefault(x => x.PlayerId <= shooter.PlayerId && (x.SatOutInARow >= 3 || x.CurrentMoney == 0 || AllNemesisHaveDropped(x)));
                //    if (playerToRemove != null)
                //    {
                //        PlayersInGame.Remove(playerToRemove);
                //        if (PlayersInGame.Count == 1)
                //        {
                //            PlayersInGame[0].CurrentMoney += Pot;
                //            Pot = 0;
                //            return;
                //        }
                //    }
                //    else
                //        break;
                //}
                //shooter = GetNextPlayer(shooter.PlayerId);


                //var playersToRemove = new List<Player>();
                //foreach (var player in PlayersInGame.Where(x => x.PlayerId >= shooter.PlayerId).OrderBy(x => x.PlayerId))
                //{
                //    if (player.SatOutInARow >= 3 || player.CurrentMoney == 0 || AllNemesisHaveDropped(player))
                //        playersToRemove.Add(player);
                //}
                //foreach (var player in PlayersInGame.Where(x => x.PlayerId < shooter.PlayerId).OrderBy(x => x.PlayerId))
                //{
                //    if (player.SatOutInARow >= 3 || player.CurrentMoney == 0 || AllNemesisHaveDropped(player))
                //        playersToRemove.Add(player);
                //}
                //foreach (var playerToRemove in playersToRemove)
                //{
                //    PlayersInGame.Remove(playerToRemove);
                //    if (PlayersInGame.Count == 1)
                //    {
                //        PlayersInGame[0].CurrentMoney += Pot;
                //        Pot = 0;
                //        return;
                //    }
                //}
                //shooter = GetNextPlayer(shooter.PlayerId);
            }
        }

        private Player GetNextPlayer(int playerId)
        {
            if (PlayersInGame.Max(x => x.PlayerId) > playerId)
            {
                var minPlayerId = PlayersInGame.Where(x => x.PlayerId > playerId).Min(x => x.PlayerId);
                return PlayersInGame.First(x => x.PlayerId == minPlayerId);
            }
            else
            {
                return PlayersInGame.First();
            }
        }

        public bool AllNemesisHaveDropped(Player player)
        {
            foreach (var nemesis in player.Nemeses)
            {
                if (PlayersInGame.Where(x => !x.Deleted).Contains(nemesis))
                {
                    return false;
                }
            }
            //foreach (var nemesis in PlayersInGame.Where(x => !x.Deleted))
            //{
            //    if (nemesis.Nemeses.Contains(player))
            //    {
            //        return false;
            //    }
            //}
            return true;
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
