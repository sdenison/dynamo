using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Player
    {
        public List<Marble> CapturedMarbles { get; } = new List<Marble>();
        public int PlayerNumber { get; }
        public long Score { get; set; }

        public Player(int playerNumber)
        {
            PlayerNumber = playerNumber;
            Score = 0;
        }
    }
}
