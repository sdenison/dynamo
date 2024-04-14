using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.CardGames.TwentyOne
{
    public class Player
    {
        public Hand Hand { get; set; }
        public List<Hand> Hands { get; set; } = new List<Hand>();
        public bool Winner { get; set; }

        public void SplitHand(Hand hand)
        {
            Hands.Add(new Hand());
            Hands.Add(new Hand());
            Hands[0].Cards.Add(hand.Cards[0]);
            Hands[1].Cards.Add(hand.Cards[1]);
        }
    }
}
