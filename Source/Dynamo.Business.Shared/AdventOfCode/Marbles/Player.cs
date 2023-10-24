using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Marbles
{
    public class Player
    {
        public List<Marble> CapturedMarbles { get; } = new List<Marble>();
        public int PlayerNumber { get; }
        public long Score { get; set; }

        //public long Score
        //{
        //    get
        //    {
        //        return CapturedMarbles.Sum(x => x.Value);
        //    }
        //}

        public Player(int playerNumber)
        {
            PlayerNumber = playerNumber;
            Score = 0;
        }
    }
}
