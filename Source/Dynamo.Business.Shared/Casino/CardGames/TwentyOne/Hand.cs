using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Dynamo.Business.Shared.Casino.CardGames.TwentyOne
{
    public class Hand
    {
        public List<Card> Cards { get; } = new List<Card>();
        public bool Winner { get; set; } = true;

        public List<int> HandValues()
        {
            var possibleValues = new List<int>();
            var aceCount = 0;
            int totalValue = 0;

            foreach (var card in Cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                    totalValue += 1;
                }
                else
                {
                    totalValue += Math.Min((int)card.Rank, 10);
                }
            }

            possibleValues.Add(totalValue);
            for (int i = 0; i < aceCount; i++)
            {
                totalValue += 10;
                possibleValues.Add(totalValue);
            }

            //Filter out results greater than 21
            return possibleValues.Distinct().Where(x => x <= 21).ToList();
        }

        public int MinValue()
        {
            return HandValues().Min();
        }

        public int MaxValue()
        {
            return HandValues().Max();
        }

        public bool IsBusted()
        {
            return HandValues().Count == 0;
        }

        public bool IsSplittable()
        {
            if (Cards[0].Rank == Cards[1].Rank)
                return true;
            if ((Cards[0].Rank == Rank.Ten || Cards[0].Rank == Rank.Jack || Cards[0].Rank == Rank.Queen || Cards[0].Rank == Rank.King)
                && (Cards[1].Rank == Rank.Ten || Cards[1].Rank == Rank.Jack || Cards[1].Rank == Rank.Queen || Cards[1].Rank == Rank.King))
                return true;
            return false;
        }
    }
}
