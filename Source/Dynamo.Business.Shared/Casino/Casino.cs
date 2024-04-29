using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Casino
{
    public class Casino
    {
        //public SortedDictionary<double, Player> Players { get; set; }
        public List<Player> Players { get; set; }
        public double CurrentTime { get; set; }
        public double TotalTime { get; set; }

        public Casino()
        {
            Players = new List<Player>();
        }

        public void AddPlayer(Player player, double timeIncrement)
        {
            var playersToRemove = Players.Where(x => x.LeavingTime < CurrentTime + timeIncrement).ToList();
            foreach (var playerToRemove in playersToRemove)
            {
                Players.Remove(playerToRemove);
            }

            player.LeavingTime = player.TimeInCasino + CurrentTime;
            Players.Add(player);

            CurrentTime += timeIncrement;
            while (Players.Count > 1000)
            {
                var playerToRemove = Players.OrderBy(x => x.LeavingTime).First();
                Players.Remove(playerToRemove);
                CurrentTime = playerToRemove.LeavingTime;
            }

            if (Players.Count > 1000)
            {
                throw new Exception("Player count never exceeded 1000 in the original simulation");
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
        //public Queue<Player> NextQueue { get; set; }
        public Game NextGame { get; set; }

        public Player? _currentPlayer = null;

        public string Name { get; set; }

        //This is the time each player will spend in the game
        public double[] TimeInGame { get; protected set; }
        public int _playerCount = 0;

        public Game(int playerCount, double distributionMean)
        {
            TimeInGame = MathHelper.GenerateExponentialRandomVariables(distributionMean, playerCount);
            WaitingPlayers = new Queue<Player>();
        }

        public double AddPlayer(Player player, double currentTime)
        {
            player.GameTime = TimeInGame[_playerCount];
            player.TotalGameTime += player.GameTime;

            MovePlayersThroughTheGame(currentTime);
            //MovePlayersThroughTheGame();

            //No need to wait
            if (_currentPlayer == null)
            {
                //The player never had to wait
                _currentPlayer = player;
                _currentPlayer.LeavingTime = currentTime + player.GameTime;
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

        public void MovePlayersThroughTheGame(double currentTime)
        {
            while (_currentPlayer != null && _currentPlayer.LeavingTime <= currentTime)
            {
                var lastPlayer = _currentPlayer;
                var lastPlayerLeavingTime = _currentPlayer.LeavingTime;
                if (NextGame != null)
                {
                    NextGame.AddPlayer(lastPlayer, lastPlayer.LeavingTime);
                }
                if (WaitingPlayers.Any())
                {
                    _currentPlayer = WaitingPlayers.Dequeue();
                    _currentPlayer.LeavingTime = lastPlayerLeavingTime + _currentPlayer.GameTime;
                    _currentPlayer.WaitTime += lastPlayerLeavingTime - _currentPlayer.LastWaitTimeStart;
                }
                else
                {
                    _currentPlayer = null;
                }
            }
        }

        public void MovePlayersThroughTheGame()
        {
            while (_currentPlayer != null)
            {
                var lastPlayer = _currentPlayer;
                var lastPlayerLeavingTime = _currentPlayer.LeavingTime;
                if (NextGame != null)
                {
                    NextGame.AddPlayer(lastPlayer, lastPlayer.LeavingTime);
                }
                if (WaitingPlayers.Any())
                {
                    _currentPlayer = WaitingPlayers.Dequeue();
                    _currentPlayer.LeavingTime = lastPlayerLeavingTime + _currentPlayer.GameTime;
                    _currentPlayer.WaitTime += lastPlayerLeavingTime - _currentPlayer.LastWaitTimeStart;
                }
                else
                {
                    _currentPlayer = null;
                }
            }

            if (NextGame != null)
                NextGame.MovePlayersThroughTheGame();
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
            Blackjack.Name = "Blackjack";
            Roulette = new Game(playerCount, 0.15); //9 minutes
            Roulette.Name = "Roulette";
            Craps = new Game(playerCount, 0.1416666666667); //8.5 minutes
            Craps.Name = "Craps";

            Blackjack.NextGame = Roulette;
            Roulette.NextGame = Craps;
        }

        public void AddPlayer(Player player, double currentTime)
        {
            Blackjack.AddPlayer(player, currentTime);
        }

        public void MovePlayersThroughTheGame()
        {
            Blackjack.MovePlayersThroughTheGame();
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

    public class PoissonRandom
    {
        private Random _random;

        public PoissonRandom()
        {
            _random = new Random();
        }

        public int GeneratePoisson(double lambda)
        {
            double L = Math.Exp(-lambda);
            int k = 0;
            double p = 1.0;

            do
            {
                k++;
                p *= _random.NextDouble();
            } while (p > L);

            return k - 1;
        }
    }

}
