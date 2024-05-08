namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Bet
    {
        public int Amount { get; set; }
        public SpaceType Space { get; private set; }
        public bool MainBet { get; private set; }

        public Bet(int amount, SpaceType betType, bool mainBet)
        {
            Amount = amount;
            Space = betType;
            MainBet = mainBet;
        }
    }

    public enum BetType
    {
        CalledShot,
        AllForOne
    }
}
