using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.Casino.CardGames;
using Dynamo.Business.Shared.Casino.CardGames.TwentyOne;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Casino.CardGames.TwentyOne
{
    public class GameTests
    {

        [Test]
        public void Can_play_21()
        {
            var game = new Game();
            //Play the game with the deck unshuffled
            game.PlayGameV1();
            Assert.That(game.Dealer.Hand.Cards[0].Rank, Is.EqualTo(Rank.Two));
            Assert.That(game.Dealer.Hand.Cards[0].Suit, Is.EqualTo(Suit.Diamonds));
            Assert.That(game.Dealer.Hand.Cards[1].Rank, Is.EqualTo(Rank.Three));
            Assert.That(game.Dealer.Hand.Cards[1].Suit, Is.EqualTo(Suit.Diamonds));
            Assert.That(game.Player.Hand.Cards[0].Rank, Is.EqualTo(Rank.Four));
            Assert.That(game.Player.Hand.Cards[0].Suit, Is.EqualTo(Suit.Diamonds));
            Assert.That(game.Player.Hand.Cards[1].Rank, Is.EqualTo(Rank.Five));
            Assert.That(game.Player.Hand.Cards[1].Suit, Is.EqualTo(Suit.Diamonds));
        }

        [Test]
        public void A_Hand_can_calculate_the_value_of_itself_2_plus_7()
        {
            var hand = new Hand();
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Two, Suit = Suit.Hearts });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Five, Suit = Suit.Diamonds });
            Assert.That(hand.HandValues()[0], Is.EqualTo(7));
        }

        [Test]
        public void A_Hand_can_calculate_the_value_of_itself_with_aces()
        {
            var hand = new Hand();
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Two, Suit = Suit.Hearts });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Five, Suit = Suit.Diamonds });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Ace, Suit = Suit.Diamonds });
            Assert.That(hand.HandValues().Count, Is.EqualTo(2));
            Assert.That(hand.HandValues()[0], Is.EqualTo(8));
            Assert.That(hand.HandValues()[1], Is.EqualTo(18));
        }

        [Test]
        public void A_Hand_can_calculate_the_value_of_itself_with_three_aces()
        {
            var hand = new Hand();
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Two, Suit = Suit.Hearts });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Five, Suit = Suit.Diamonds });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Ace, Suit = Suit.Diamonds });
            hand.Cards.Add(new Card() { Id = Guid.NewGuid(), Rank = Rank.Ace, Suit = Suit.Hearts });
            Assert.That(hand.HandValues().Count, Is.EqualTo(2)); //This is filtering hand values over 21
            Assert.That(hand.HandValues()[0], Is.EqualTo(9));
            Assert.That(hand.HandValues()[1], Is.EqualTo(19));
        }

        [Test]
        public void If_the_dealer_is_dealt_21_then_they_win()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            Assert.That(game.Deck.Cards[0].Rank, Is.EqualTo(Rank.Ten));
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ace, 1);
            Assert.That(game.Deck.Cards[1].Rank, Is.EqualTo(Rank.Ace));

            //Stack the deck for the player
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Ten, 2);
            Assert.That(game.Deck.Cards[2].Rank, Is.EqualTo(Rank.Ten));
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Ace, 3);
            Assert.That(game.Deck.Cards[1].Rank, Is.EqualTo(Rank.Ace));

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.False);
        }

        [Test]
        public void Player_wins_at_21()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Jack, 1);

            //Stack the deck for the player
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Ten, 2);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Ace, 3);

            game.PlayGameV2();

            Assert.That(game.Player.Hands[0].Winner, Is.True);
        }

        [Test]
        public void Player_has_to_keep_hitting_until_over_15()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 17
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Six, 1);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 3 = 19
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Three, 5);

            game.PlayGameV1();
            Assert.That(game.Player.Winner, Is.True);
        }

        [Test]
        public void Player_cant_hit_over_15()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 19
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Nine, 1);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 2 = 18
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Two, 5);

            game.PlayGameV1();
            Assert.That(game.Player.Winner, Is.False);
        }

        [Test]
        public void Player_loses_when_they_go_bust()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 19
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Nine, 1);

            //Stack the deck for the player
            //This gives the player 4, 5, 5, 8 = 22
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Five, 4);
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Eight, 5);

            game.PlayGameV1();
            Assert.That(game.Player.Winner, Is.False);
        }

        [Test]
        public void Get_week_1_part_1_answer()
        {
            var playerWins = 0;
            var gamesPlayed = 0;
            var gamesToPlay = 200000;

            while (gamesPlayed < gamesToPlay)
            {
                var game = new Game();
                game.Deck.Shuffle();
                game.PlayGameV1();
                if (game.Player.Winner)
                    playerWins++;
                gamesPlayed++;
            }

            var playerWinPercent = (float)playerWins / gamesToPlay;

            Assert.That(playerWinPercent, Is.GreaterThan(0.6));
            Assert.That(playerWinPercent, Is.LessThan(0.61));
        }

        [Test]
        public void Dealer_can_also_play()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 16
            //This gives the dealer 10, 6, 4
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Six, 1);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Four, 6);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 4 = 19
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Four, 5);

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.False);
        }

        [Test]
        public void Player_wins_when_dealer_busts()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 16
            //This gives the dealer 10, 6, 4
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Six, 1);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Nine, 6);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 4 = 19
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Four, 5);

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.True);
        }

        [Test]
        public void Player_wins_when_both_have_to_hit_but_players_hand_is_better()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 16
            //This gives the dealer 10, 6, 4
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Six, 1);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Two, 6);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 4 = 19
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Four, 5);

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.True);
        }

        [Test]
        public void Player_cant_hit_over_15_and_dealer_can_hit_under_17()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 16
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Six, 1);
            //This hit will give then 21
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Five, 6);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 2 = 21
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Six, 5);

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.False);
        }

        [Test]
        public void Player_cant_hit_over_15_v2()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 19
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Nine, 1);

            //Stack the deck for the player
            //This gives the player 4, 5, 6, 2 = 18
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Two, 5);

            game.PlayGameV2();
            Assert.That(game.Player.Hands[0].Winner, Is.False);
        }

        [Test]
        public void Player_can_split_hands()
        {
            //Create a game and don't shuffle
            var game = new Game();

            //Stack the deck for the dealer. Give them 19
            game.Deck.PutCardInPosition(Suit.Diamonds, Rank.Ten, 0);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Nine, 1);

            //Stack the deck for the player
            //This gives the player 10, jack, 6, 2 = 18
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Ten, 2);
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Jack, 3);
            //Gives the first hand 17
            game.Deck.PutCardInPosition(Suit.Spades, Rank.Seven, 4);
            //Gives the second hand 19
            game.Deck.PutCardInPosition(Suit.Hearts, Rank.Ten, 5);

            game.PlayGameV2();

            //Make sure we have split hands
            Assert.That(game.Player.Hands.Count, Is.EqualTo(2));

            //First hand loses
            Assert.That(game.Player.Hands[0].Winner, Is.False);

            //Second hand wins
            Assert.That(game.Player.Hands[1].Winner, Is.True);
        }


        [Test]
        public void Get_week_1_part_2_answer()
        {
            var gamesPlayed = 0;
            var gamesToPlay = 200000;
            var handsWon = 0;

            while (gamesPlayed < gamesToPlay)
            {
                var game = new Game();
                game.Deck.Shuffle();
                game.PlayGameV2();
                handsWon += game.Player.Hands.Count(x => x.Winner);
                gamesPlayed++;
            }

            var playerWinPercent = (float)handsWon / gamesToPlay;

            Assert.That(playerWinPercent, Is.GreaterThan(0.44));
            Assert.That(playerWinPercent, Is.LessThan(0.445));
        }

        [Test]
        public void Get_challenge_3_part_1()
        {
            var gamesPlayed = 0;
            var gamesToPlay = 200000;
            var handsWon = 0;
            var doubleBustCount = 0;

            while (gamesPlayed < gamesToPlay)
            {
                var game = new Game();
                game.Deck.Shuffle();
                game.PlayGameV2();
                handsWon += game.Player.Hands.Count(x => x.Winner);
                if (game.Dealer.Hand.DoubleBust)
                    doubleBustCount++;
                gamesPlayed++;
            }

            var doubleBustPercent = (float)doubleBustCount / gamesToPlay;

            //Accepted answer was 0.67
            Assert.That(doubleBustPercent, Is.GreaterThan(0.065));
            Assert.That(doubleBustPercent, Is.LessThan(0.068));
        }

        [Test]
        public void Get_challenge_3_part_2()
        {
            // Initialize counters for the game simulation
            var gamesPlayed = 0;
            var gamesToPlay = 200000;

            // Run the simulation for the specified number of games
            var doubleBustResults = new List<bool>();
            while (gamesPlayed < gamesToPlay)
            {
                // Create and set up a new game
                var game = new Game();
                game.Deck.Shuffle();
                game.PlayGameV2();

                // Check if both the dealer and player have busted
                doubleBustResults.Add(game.Dealer.Hand.DoubleBust);

                gamesPlayed++;
            }

            // Calculate the percentage of double busts
            var doubleBustPercent = (float)doubleBustResults.Count(x => x) / gamesToPlay;

            // Perform bootstrap resampling to calculate the confidence interval
            var bootstrapSamples = 1000;
            var random = new Random();
            var bootstrapMeans = new List<float>();

            for (int i = 0; i < bootstrapSamples; i++)
            {
                var sample = new List<bool>();
                for (int j = 0; j < gamesToPlay; j++)
                {
                    sample.Add(doubleBustResults[random.Next(gamesToPlay)]);
                }
                var sampleMean = (float)sample.Count(x => x) / gamesToPlay;
                bootstrapMeans.Add(sampleMean);
            }

            // Calculate the 2.5th and 97.5th percentiles for the 95% confidence interval
            bootstrapMeans.Sort();
            var lowerBound = bootstrapMeans[(int)(0.025 * bootstrapSamples)];
            var upperBound = bootstrapMeans[(int)(0.975 * bootstrapSamples)];

            //Accepted answer was 0.065
            //Assert.That(lowerBound, Is.EqualTo(0.06492));
            //Accepted answer was 0.068
            //Assert.That(upperBound, Is.EqualTo(0.06797));
        }
    }
}
