using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Game
    {
        public int NumberOfPlayers { get; }
        public int LastMarbleValue { get; }
        public List<long> Players { get; }
        public List<Marble> Marbles { get; }

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
            //Marbles = new List<Marble>();
            //for (int i = 3; i <= lastMarbleValue; i++)
            //    Marbles.Add(new Marble(i));
        }

        public void PlayGame()
        {
            //Initialize
            var circle = new List<int>();
            circle.Add(0);
            circle.Add(1);
            circle.Add(2);
            var currentMarble = circle[1];
            var currentMarbleIndex = 1;

            var currentPlayerId = 0;
            for (var marble = 23; marble <= LastMarbleValue; marble += 23)
            {
                currentPlayerId = (marble - 3) % NumberOfPlayers;
                Players[currentPlayerId] += marble;
            }

            var turns = 0;
            currentPlayerId = 0;
            for (var marble = 3; marble <= LastMarbleValue; marble++)
            //foreach (var marble in Marbles)
            {
                //turns++;
                if (marble % 23 == 0)
                {
                    //Players[currentPlayerId] += marble;
                    //var marbleToRemoveId = circle.IndexOf(currentMarble) - 7;
                    var marbleToRemoveId = currentMarbleIndex - 7;
                    if (marbleToRemoveId < 0)
                        marbleToRemoveId = (circle.Count - 1) - ((marbleToRemoveId + 1) * -1);
                    var marbleToRemove = circle[marbleToRemoveId];
                    Players[currentPlayerId] += marbleToRemove;
                    circle.Remove(marbleToRemove);
                    //currentMarble = circle[marbleToRemoveId];
                    currentMarbleIndex = marbleToRemoveId;
                }
                else
                {
                    //var circleCount = circle.Count;
                    //if (circle.IndexOf(currentMarble) == circleCount - 1)
                    //    circle.Insert(1, marble);
                    //else
                    //    circle.Insert(circle.IndexOf(currentMarble) + 2, marble);
                    //currentMarble = marble;

                    //var currentMarbleIndexCheck = circle.IndexOf(currentMarble);

                    if (currentMarbleIndex == circle.Count - 1)
                        currentMarbleIndex = 1;
                    else
                        currentMarbleIndex += 2;
                    circle.Insert(currentMarbleIndex, marble);

                    //if (currentMarbleIndexCheck != currentMarbleIndex)
                    //{
                    //    var x = "Oh no";
                    //}
                }
                //}
                currentPlayerId++;
                if (currentPlayerId == NumberOfPlayers)
                    currentPlayerId = 0;
            }
        }
    }
}
