using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
