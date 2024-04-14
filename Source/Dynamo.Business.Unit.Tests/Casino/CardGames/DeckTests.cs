using Dynamo.Business.Shared.Casino.CardGames;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.Casino.CardGames
{
    public class DeckTests
    {
        [Test]
        public void Can_crate_deck_of_cards()
        {
            var deck = new Deck();
            Assert.That(deck.Cards.Count, Is.EqualTo(52));
        }

        [Test]
        public void Can_shuffle_the_deck()
        {
            var deck = new Deck();
            var firstThreeCardsBeforeShuffle = deck.Cards.Take(3).ToList();
            Assert.That(deck.Cards[0].Suit, Is.EqualTo(Suit.Diamonds));
            Assert.That(deck.Cards[0].Rank, Is.EqualTo(Rank.Two));
            deck.Shuffle();
            var firstThreeCardsAfterShuffle = deck.Cards.Take(3).ToList();
            var isOrderChanged = !firstThreeCardsBeforeShuffle.SequenceEqual(firstThreeCardsAfterShuffle);
            Assert.That(isOrderChanged, Is.True);
        }
    }
}
