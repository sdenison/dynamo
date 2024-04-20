using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Dynamo.Business.Shared.Casino.Slots
{
    public class MechanicalSlotMachine
    {
        public List<Reel> Reels { get; set; }
        public List<Payout> Payouts { get; private set; }
        public long Money { get; private set; }

        public MechanicalSlotMachine(List<string> reelStrings, List<Payout> payouts, long money)
        {
            Payouts = payouts;
            Reels = reelStrings.Select(x => new Reel(x)).ToList();
            Money = money;
        }

        public int PullHandle()
        {
            Money -= 1;
            var random = new Random();
            foreach (var reel in Reels)
            {
                var randomNumber = random.Next(0, reel.NumberOfSymbols);
                reel.Next(randomNumber);
            }
            foreach (var payout in Payouts.OrderByDescending(x => x.WinAmount))
            {
                var winner = true;
                foreach (var reel in Reels)
                {
                    if (!payout.Symbols.Contains(reel.CurrentSymbol.Symbol))
                        winner = false;
                }
                if (winner)
                {
                    Money += payout.WinAmount;
                    return payout.WinAmount;
                }
            }
            return 0;
        }

        public void AdvanceReel()
        {
            foreach (var reel in Reels)
            {
                reel.Next();
            }
        }

        public int HandlePayout()
        {
            Money -= 1;
            foreach (var payout in Payouts.OrderByDescending(x => x.WinAmount))
            {
                var winner = true;
                foreach (var reel in Reels)
                {
                    if (!payout.Symbols.Contains(reel.CurrentSymbol.Symbol))
                        winner = false;
                }
                if (winner)
                {
                    Money += payout.WinAmount;
                    return payout.WinAmount;
                }
            }
            return 0;
        }

        public void PullHandleNumberOfTimes(int timesToPullHandle)
        {
            for (var i = 0; i < timesToPullHandle; i++)
            {
                if (Money > 0)
                    PullHandle();
                else
                    return;
            }
        }
    }

    public class Reel
    {
        public ReelSymbol CurrentSymbol { get; private set; }
        public int NumberOfSymbols { get; }

        public void Next()
        {
            CurrentSymbol = CurrentSymbol.NextSymbol;
        }

        public async Task NextAsync(int delay)
        {
            Next();
            await Task.Delay(delay);
            var x = "got here";
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
            NumberOfSymbols = symbols.Count;

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

            CurrentSymbol.NextSymbol = firstSymbol;
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
