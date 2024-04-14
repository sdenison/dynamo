using System.Linq;

namespace Dynamo.Business.Shared.Casino.CardGames.TwentyOne
{
    public class Game
    {
        public Deck Deck { get; set; } = new Deck();
        public Player Dealer { get; } = new Player();
        public Player Player { get; set; } = new Player();

        public void Shuffle()
        {
            Deck.Shuffle();
        }

        public Hand Deal()
        {
            var hand = new Hand();
            hand.Cards.Add(Deck.NextCard());
            hand.Cards.Add(Deck.NextCard());
            return hand;
        }

        public void PlayerHit()
        {
            Player.Hand.Cards.Add(Deck.NextCard());
        }

        public void DealerHit()
        {
            Dealer.Hand.Cards.Add(Deck.NextCard());
        }

        public void PlayGameV1()
        {
            Dealer.Hand = Deal();
            Player.Hand = Deal();

            var dealerValue = Dealer.Hand.HandValues().Max();

            if (dealerValue == 21)
            {
                DealerWins();
                return;
            }
            while (true)
            {
                //Bust
                if (Player.Hand.IsBusted())
                {
                    DealerWins();
                    break;
                }

                if (Player.Hand.MaxValue() > 15)
                {
                    if (Player.Hand.HandValues().Max() > dealerValue)
                    {
                        PlayerWins();
                        break;
                    }
                }

                //Player has more hits left
                if (Player.Hand.MinValue() <= 15)
                {
                    PlayerHit();
                }
                else
                {
                    //Player is all out of hits. Time to face the music
                    if (Player.Hand.MaxValue() <= dealerValue)
                    {
                        DealerWins();
                        break;
                    }

                    PlayerWins();
                    break;
                }
            }
        }

        private void Hit(Hand hand)
        {
            hand.Cards.Add(Deck.NextCard());
        }

        public void PlayGameV2()
        {
            Dealer.Hand = Deal();

            var playerHand = Deal();
            if (playerHand.IsSplittable())
                Player.SplitHand(playerHand);
            else
                Player.Hands.Add(playerHand);


            var dealerValue = Dealer.Hand.HandValues().Max();

            if (dealerValue == 21)
            {
                DealerWins();
                Player.Hands.ForEach(x => x.Winner = false);
                return;
            }

            foreach (var hand in Player.Hands)
            {
                var hitCount = 0;
                while (true)
                {
                    //Bust
                    if (hand.IsBusted())
                    {
                        hand.Winner = false;
                        break;
                    }

                    if (hand.MaxValue() > 15)
                        break;

                    //We can only hit once for split aces
                    if (playerHand.IsSplittable() && playerHand.Cards[0].Rank == Rank.Ace && hitCount > 0)
                        break;

                    Hit(hand);
                    hitCount++;
                }
            }

            while (true)
            {
                //Bust
                if (Dealer.Hand.IsBusted())
                {
                    return;
                }

                foreach (var hand in Player.Hands.Where(x => x.Winner))
                {
                    if (Dealer.Hand.MaxValue() >= hand.MaxValue())
                        hand.Winner = false;
                }

                if (Dealer.Hand.MaxValue() > 16)
                {
                    return;
                }

                DealerHit();
            }
        }

        private void PlayerWins()
        {
            Player.Winner = true;
            Dealer.Winner = false;
        }

        private void DealerWins()
        {
            Player.Winner = false;
            Dealer.Winner = true;
        }
    }
}
