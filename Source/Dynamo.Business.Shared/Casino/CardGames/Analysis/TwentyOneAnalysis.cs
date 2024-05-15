using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Casino.CardGames.Analysis
{
    public class TwentyOneAnalysis
    {
        public List<PlayOutcome> PlayOutcomes { get; private set; }

        public TwentyOneAnalysis()
        {
            PlayOutcomes = new List<PlayOutcome>();
        }

        public static TwentyOneAnalysis Parse(string[] strings)
        {
            var twentyOneAnalysis = new TwentyOneAnalysis();
            for (var i = 1; i < strings.Length - 1; i++)
            {
                var stringValues = strings[i].Split(',');
                twentyOneAnalysis.PlayOutcomes.Add(new PlayOutcome
                {
                    PlayerNo = stringValues[1],
                    Card1 = int.Parse(stringValues[2]),
                    Card2 = int.Parse(stringValues[3]),
                    Card3 = int.Parse(stringValues[4]),
                    Card4 = int.Parse(stringValues[5]),
                    Card5 = int.Parse(stringValues[6]),
                    SumOfCards = int.Parse(stringValues[7]),
                    DealerCard1 = int.Parse(stringValues[8]),
                    DealerCard2 = int.Parse(stringValues[9]),
                    DealerCard3 = int.Parse(stringValues[10]),
                    DealerCard4 = int.Parse(stringValues[11]),
                    DealerCard5 = int.Parse(stringValues[12]),
                    SumOfDeal = int.Parse(stringValues[13]),
                    BlackJack = (BlackJack)Enum.Parse(typeof(BlackJack), stringValues[14], true),
                    WinLoss = (WinLoss)Enum.Parse(typeof(WinLoss), stringValues[15], true),
                    PlayerBustBeat = (PlayerBustBeat)Enum.Parse(typeof(PlayerBustBeat), stringValues[16], true),
                    DealerBustBeat = (DealerBustBeat)Enum.Parse(typeof(DealerBustBeat), stringValues[17], true),
                    PlayerWinAmount = int.Parse(stringValues[18]),
                    DealerWinAmount = int.Parse(stringValues[19]),
                    SumOfCards2 = int.Parse(stringValues[20]),
                });
            }
            return twentyOneAnalysis;
        }

        public List<PlayOutcome> Resample()
        {
            var random = new Random();
            var sample = new List<PlayOutcome>();
            for (int i = 0; i < PlayOutcomes.Count; i++)
            {
                int index = random.Next(PlayOutcomes.Count);
                sample.Add(PlayOutcomes[index]);
            }
            return sample;
        }
    }

    public class PlayOutcome
    {
        public string PlayerNo { get; set; }
        public int Card1 { get; set; }
        public int Card2 { get; set; }
        public int Card3 { get; set; }
        public int Card4 { get; set; }
        public int Card5 { get; set; }
        public int SumOfCards { get; set; }
        public int DealerCard1 { get; set; }
        public int DealerCard2 { get; set; }
        public int DealerCard3 { get; set; }
        public int DealerCard4 { get; set; }
        public int DealerCard5 { get; set; }
        public int SumOfDeal { get; set; }
        public BlackJack BlackJack { get; set; }
        public WinLoss WinLoss { get; set; }
        public PlayerBustBeat PlayerBustBeat { get; set; }
        public DealerBustBeat DealerBustBeat { get; set; }
        public int PlayerWinAmount { get; set; }
        public int DealerWinAmount { get; set; }
        public int SumOfCards2 { get; set; } //sum of the first two player cards. no idea why.
    }

    public enum BlackJack
    {
        NoWin,
        Win
    }

    public enum WinLoss
    {
        Win,
        Loss,
        Push
    }

    public enum PlayerBustBeat
    {
        Beat,
        PlWin,
        DlWin,
        Bust,
        DlBust,
        Push
    }

    public enum DealerBustBeat
    {
        DlWin,
        Beat,
        PlBust,
        Bust,
        Push
    }
}
