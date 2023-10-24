using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Game
    {
        public int NumberOfPlayers { get; }
        public int LastMarbleValue { get; }
        public List<Player> Players { get; }
        public List<Marble> Marbles { get; }

        public int HighScore
        {
            get
            {
                return Players.OrderByDescending(x => x.Score).First().Score;
            }
        }


        public Game(string gameDescription) : this(int.Parse(gameDescription.Split(' ')[0]), int.Parse(gameDescription.Split(' ')[6]))
        {
        }

        public Game(int numberOfPlayers, int lastMarbleValue)
        {
            NumberOfPlayers = numberOfPlayers;
            LastMarbleValue = lastMarbleValue;
            Players = new List<Player>();
            for (int i = 1; i <= numberOfPlayers; i++)
                Players.Add(new Player(i));
            Marbles = new List<Marble>();
            for (int i = 1; i <= lastMarbleValue; i++)
                Marbles.Add(new Marble(i));
        }

        public void PlayGame()
        {
            //Initialize
            var circle = new List<Marble>();
            circle.Add(new Marble(0));
            var currentMarble = circle[0];

            var turns = 0;
            var currentPlayerId = 0;
            foreach (var marble in Marbles)
            {
                turns++;
                if (turns <= 2)
                {
                    circle.Insert(1, marble);
                    currentMarble = marble;
                }
                else
                {
                    if (marble.Value % 23 == 0)
                    {
                        Players[currentPlayerId].CapturedMarbles.Add(marble);
                        var marbleToRemoveId = circle.IndexOf(currentMarble) - 7;
                        if (marbleToRemoveId < 0)
                            marbleToRemoveId = (circle.Count - 1) - ((marbleToRemoveId + 1) * -1);
                        var marbleToRemove = circle[marbleToRemoveId];
                        Players[currentPlayerId].CapturedMarbles.Add(marbleToRemove);
                        circle.Remove(marbleToRemove);
                        currentMarble = circle[marbleToRemoveId];
                    }
                    else
                    {

                        if (circle.IndexOf(currentMarble) == circle.Count - 1)
                            circle.Insert(1, marble);
                        else
                            circle.Insert(circle.IndexOf(currentMarble) + 2, marble);
                        currentMarble = marble;
                    }
                }
                currentPlayerId++;
                if (currentPlayerId == NumberOfPlayers)
                    currentPlayerId = 0;
            }
        }
    }
}
