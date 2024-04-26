using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Business.Shared.AdventOfCode.Guard;

namespace Dynamo.Business.Shared.Casino
{
    public class Casino
    {
        public SortedDictionary<double, Player> Players { get; set; }
        public double CurrentTime { get; set; }
        public double TotalTime { get; set; }

        public Casino()
        {
            Players = new SortedDictionary<double, Player>();
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count >= 1000)
            {
                var nextPlayerToExit = Players.FirstOrDefault();
                TotalTime = CurrentTime + nextPlayerToExit.Key;
                Players.Remove(nextPlayerToExit.Key);
            }
            var exitTime = player.TimeInCasino + CurrentTime;
            Players.Add(exitTime, player);
            CurrentTime += 0.0021739130;

            var playersToRemove = Players.Where(x => x.Key < CurrentTime).ToList();
            foreach (var playerToRemove in playersToRemove)
            {
                Players.Remove(playerToRemove.Key);
            }
        }
    }


    public class Player
    {
        public double TimeInCasino { get; set; }
        public double LeavingTime { get; set; }
        public double GameTime { get; set; }
        public double TotalGameTime { get; set; }
        public double WaitTime { get; set; }
        public double LastWaitTimeStart { get; set; }
    }

    public class Game
    {
        public Queue<Player> WaitingPlayers { get; set; }
        public Queue<Player> NextQueue { get; set; }
        public Game NextGame { get; set; }

        private Player? _currentPlayer = null;

        //This is the time each player will spend in the game
        public double[] TimeInGame { get; protected set; }
        private int _playerCount = 0;

        public Game(int playerCount, double distributionMean)
        {
            TimeInGame = MathHelper.GenerateExponentialRandomVariables(distributionMean, playerCount);
            NextQueue = new Queue<Player>();
            WaitingPlayers = new Queue<Player>();
        }

        public Game(int playerCount, double distributionMean, Game previousGame)
        {
            TimeInGame = MathHelper.GenerateExponentialRandomVariables(distributionMean, playerCount);
            NextQueue = new Queue<Player>();
            WaitingPlayers = previousGame.NextQueue;
        }

        public double AddPlayer(Player player, double currentTime)
        {
            player.GameTime = TimeInGame[_playerCount];
            player.TotalGameTime += player.GameTime;

            MovePlayersThroughTheGame(currentTime);

            //No need to wait
            if (_currentPlayer == null)
            {
                //The player never had to wait
                _currentPlayer = player;
                _currentPlayer.LeavingTime = currentTime + TimeInGame[_playerCount];
                //return currentTime;
            }
            else
            {
                //Add the player to the waiting player's queue
                player.LastWaitTimeStart = currentTime;
                WaitingPlayers.Enqueue(player);
            }

            //Add the player to the waiting player's queue
            //player.LastWaitTimeStart = currentTime;
            //WaitingPlayers.Enqueue(player);

            //MovePlayersThroughTheGame(currentTime);

            _playerCount++;
            return currentTime;
        }

        private void MovePlayersThroughTheGame(double currentTime)
        {
            while (_currentPlayer != null && _currentPlayer.LeavingTime <= currentTime)
            {
                var lastPlayer = _currentPlayer;
                NextQueue.Enqueue(_currentPlayer);
                if (NextGame != null)
                {
                    NextGame.AddPlayer(lastPlayer, lastPlayer.LeavingTime);
                }
                if (WaitingPlayers.Any())
                {
                    _currentPlayer = WaitingPlayers.Dequeue();
                    _currentPlayer.LeavingTime = lastPlayer.LeavingTime + _currentPlayer.GameTime;
                    _currentPlayer.WaitTime += lastPlayer.LeavingTime - _currentPlayer.LastWaitTimeStart;
                }
                else
                {
                    _currentPlayer = null;
                }
            }
        }
    }

    public class GameRoom
    {
        public Game Blackjack { get; private set; }
        public Game Roulette { get; private set; }
        public Game Craps { get; private set; }
        public GameRoom(int playerCount)
        {
            Blackjack = new Game(playerCount, 0.1666666667); //10 minutes
            Roulette = new Game(playerCount, 0.15); //9 minutes
            Craps = new Game(playerCount, 0.1416666666667); //8.5 minutes

            Blackjack.NextGame = Roulette;
            Roulette.NextGame = Craps;
        }

        public void AddPlayer(Player player, double currentTime)
        {
            Blackjack.AddPlayer(player, currentTime);
        }
    }

    public static class MathHelper
    {
        public static double[] GenerateExponentialRandomVariables(double scale, int size)
        {
            var random = new Random();
            double[] values = new double[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = -scale * Math.Log(1 - random.NextDouble());
            }
            return values;
        }
    }

}
