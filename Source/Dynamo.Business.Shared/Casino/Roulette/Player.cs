using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Player
    {
        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}
