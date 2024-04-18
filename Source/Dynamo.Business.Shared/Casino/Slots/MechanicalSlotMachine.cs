using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.Casino.Slots
{
    public class MechanicalSlotMachine
    {
        public List<Reel> Reels { get; set; }
        public List<Payout> Payouts { get; private set; }

        public MechanicalSlotMachine(List<string> reelStrings, List<Payout> payouts)
        {
            Payouts = payouts;
            Reels = new List<Reel>();
            foreach (var reelString in reelStrings)
            {
                Reels.Add(new Reel(reelString));
            }
        }
    }

    public class Reel
    {
        public ReelSymbol CurrentSymbol { get; private set; }

        public void Next()
        {
            CurrentSymbol = CurrentSymbol.NextSymbol;
        }

        public void Next(int numberToMove)
        {
            for (var i = 0; i < numberToMove; i++)
            {
                CurrentSymbol = CurrentSymbol.NextSymbol;
            }
        }

        public Reel(string symbolsString) : this(ConvertStringToSymbols(symbolsString))
        {
        }

        public Reel(List<Symbol> symbols)
        {
            if (symbols == null || symbols.Count == 0)
                throw new ArgumentException("Symbols list cannot be empty.", nameof(symbols));

            var firstSymbol = new ReelSymbol()
            {
                Symbol = symbols[0]
            };
            CurrentSymbol = firstSymbol;

            foreach (var currentSymbol in symbols.Skip(1))
            {
                var reelSymbol = new ReelSymbol()
                {
                    Symbol = currentSymbol,
                    PreviousSymbol = CurrentSymbol
                };
                CurrentSymbol.NextSymbol = reelSymbol;
                CurrentSymbol = reelSymbol;
            }
            firstSymbol.PreviousSymbol = CurrentSymbol;
            CurrentSymbol = firstSymbol;
        }

        private static List<Symbol> ConvertStringToSymbols(string symbolsString)
        {
            List<string> symbolNames = symbolsString.Split(',').Select(s => s.Trim()).ToList();
            List<Symbol> symbolsAsEnum = symbolNames
                .Select(s => s.Replace(" ", "").Replace("13", "Thirteen"))
                .Select(s =>
                {
                    Symbol result;
                    if (Enum.TryParse(s, true, out result))
                        return result;
                    else
                        throw new ArgumentException($"Invalid symbol name: {s}");
                })
                .ToList();

            return symbolsAsEnum;
        }
    }

    public class Payout
    {
        public List<Symbol> Symbols { get; }
        public int WinAmount { get; }

        public Payout(List<Symbol> symbols, int winAmount)
        {
            Symbols = symbols;
            WinAmount = winAmount;
        }
    }

    public class ReelSymbol
    {
        public Symbol Symbol { get; set; }
        public ReelSymbol NextSymbol { get; set; }
        public ReelSymbol PreviousSymbol { get; set; }
    }

    public enum Symbol
    {
        Guitar,
        HeartHands,
        Thirteen,
        RedLips,
        RedScarf,
        StatueOfLiberty,
        Snake,
        Butterfly,
        Cardigan,
        Champagne,
        FriendshipBracelet,
        QuillAndInk
    }
}
