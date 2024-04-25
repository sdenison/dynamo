using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }

    public abstract class Game
    {

    }

    public class Blackjack : Game
    {
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
