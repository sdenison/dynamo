using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Dynamo.Business.Shared.Casino.CardGames
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            Cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card card = new Card
                    {
                        Id = Guid.NewGuid(),
                        Suit = suit,
                        Rank = rank
                    };
                    Cards.Add(card);
                }
            }
        }

        //This is how we can stack the deck for testing
        public void PutCardInPosition(Suit suit, Rank rank, int position)
        {
            var card = Cards.Single(x => x.Suit == suit && x.Rank == rank);
            var indexOf = Cards.IndexOf(card);
            var temp = Cards[position];
            Cards[position] = card;
            Cards[indexOf] = temp;
        }

        public void Shuffle()
        {
            foreach (var card in Cards)
            {
                card.Id = Guid.NewGuid();
            }
            Cards = Cards.OrderBy(x => x.Id).ToList();
        }

        public Card NextCard()
        {
            var nextCard = Cards.First();
            Cards.Remove(nextCard);
            return nextCard;
        }
    }
}
