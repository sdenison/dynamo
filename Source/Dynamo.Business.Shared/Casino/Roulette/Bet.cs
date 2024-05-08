namespace Dynamo.Business.Shared.Casino.Roulette
{
    public abstract class Bet
    {
        public int Amount { get; protected set; }

        public Bet(int amount)
        {
            Amount = amount;
        }

        public abstract int GetPayout(SpaceType winningSpace);
    }

    public class CalledShotBet : Bet
    {
        public SpaceType Space { get; private set; }

        public CalledShotBet(int amount, SpaceType space) : base(amount)
        {
            Space = space;
        }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == Space)
                return Amount + Amount;
            return 0;
        }
    }

    public class OddsBet : Bet
    {
        public OddsBet(int amount) : base(amount) { }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == SpaceType.AllForOne)
                return 0;
            if ((int)winningSpace % 2 == 1)
                return Amount + (Amount * 3);
            return 0;
        }
    }

    public class EvensBet : Bet
    {
        public EvensBet(int amount) : base(amount) { }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == SpaceType.AllForOne)
                return 0;
            if ((int)winningSpace % 2 == 0)
                return Amount + (Amount * 3);
            return 0;
        }
    }

    public class HandBet : Bet
    {
        public HandBet(int amount) : base(amount) { }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == SpaceType.AllForOne)
                return 0;
            if ((int)winningSpace >= 1 && (int)winningSpace <= 5)
                return Amount + (Amount * 3);
            return 0;
        }
    }

    public class BushBet : Bet
    {
        public BushBet(int amount) : base(amount) { }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == SpaceType.AllForOne)
                return 0;
            if ((int)winningSpace >= 6 && (int)winningSpace <= 10)
                return Amount + (Amount * 4);
            return 0;
        }
    }

    public class AllForOneBet : Bet
    {
        public AllForOneBet(int amount) : base(amount) { }

        public override int GetPayout(SpaceType winningSpace)
        {
            if (winningSpace == SpaceType.AllForOne)
                return Amount + (Amount * 2);
            return 0;
        }
    }

    public enum BetType
    {
        CalledShot,
        Odds,
        Evens,
        TheHand,
        TheBush,
        AllForOne
    }
}
