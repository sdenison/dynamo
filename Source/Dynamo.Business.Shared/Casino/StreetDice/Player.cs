using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.Casino.StreetDice
{
    public class Player
    {
        public int PlayerId { get; private set; }
        public int CurrentMoney { get; set; }
        public double MaximumPercentage { get; private set; }
        public List<Player> Nemeses { get; set; }
        private List<int> NemesisIds { get; }
        public int MaxComfortableBet => (int)Math.Ceiling(MaximumPercentage * CurrentMoney);
        public int SatOutInARow { get; set; } = 0;
        public Player NextPlayer { get; set; }
        public Player PreviousPlayer { get; set; }


        public static Player Parse(string playerString)
        {
            var playerId = int.Parse(playerString.Substring(0, playerString.IndexOf(':')));
            var currentMoney = int.Parse(playerString.Split(' ')[1].Replace("$", ""));
            var percentInt = int.Parse(playerString.Split(' ')[2].Replace("%", " "));
            var maximumPercentage = (double)int.Parse(playerString.Split(' ')[2].Replace("%", " ")) / 100;
            var nemesisIds = playerString.Split("-")[1].Split(",").Select(x => int.Parse(x)).ToList();
            return new Player(playerId, currentMoney, maximumPercentage, nemesisIds);
        }

        public Player(int playerId, int currentMoney, double maximumPercentage, List<int> nemesisIds)
        {
            PlayerId = playerId;
            CurrentMoney = currentMoney;
            MaximumPercentage = maximumPercentage;
            NemesisIds = nemesisIds;
            Nemeses = new List<Player>();
        }

        public void LoadNemesises(List<Player> players)
        {
            foreach (var nemesisId in NemesisIds)
            {
                Nemeses.Add(players.First(x => x.PlayerId == nemesisId));
            }
        }
    }
}
