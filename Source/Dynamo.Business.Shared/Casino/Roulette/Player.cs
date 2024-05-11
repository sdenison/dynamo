using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Player
    {
        public List<Bet> Bets { get; set; } = new List<Bet>();
        public int Amount { get; set; } = 0;
    }
}
