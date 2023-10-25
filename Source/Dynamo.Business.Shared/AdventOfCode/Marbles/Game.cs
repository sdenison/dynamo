using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Game
    {
        public int NumberOfPlayers { get; }
        public int LastMarbleValue { get; }
        public List<long> Players { get; }

        public long HighScore
        {
            get
            {
                return Players.OrderByDescending(x => x).First();
            }
        }

        public Game(string gameDescription) : this(int.Parse(gameDescription.Split(' ')[0]), int.Parse(gameDescription.Split(' ')[6]))
        {
        }

        public Game(int numberOfPlayers, int lastMarbleValue)
        {
            NumberOfPlayers = numberOfPlayers;
            LastMarbleValue = lastMarbleValue;
            Players = new List<long>();
            for (int i = 1; i <= numberOfPlayers; i++)
                Players.Add(0);
        }

        public void PlayGame()
        {
            //The circle initializes with the first two marbles already played.
            var circle = new Circle();
            var currentPlayerId = 1;
            for (var marbleValue = 2; marbleValue <= LastMarbleValue; marbleValue++)
            {
                if (marbleValue % 23 == 0)
                    Players[currentPlayerId] += marbleValue + circle.CollectMarble().Value;
                else
                    circle.AddMarble(marbleValue);
                currentPlayerId++;
                if (currentPlayerId == NumberOfPlayers)
                    currentPlayerId = 0;
            }
        }
    }
}
