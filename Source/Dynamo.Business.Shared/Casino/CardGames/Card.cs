using System;

namespace Dynamo.Business.Shared.Casino.CardGames
{
    public class Card
    {
        public Guid Id { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }

    public enum Suit
    {
        Diamonds,
        Clubs,
        Spades,
        Hearts
    }

    public enum Rank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }
}
