namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Space
    {
        public SpaceType Value { get; set; }
        public ColorEnum Color { get; set; }
    }

    public enum SpaceType
    {
        CalledShot = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
    }

    public enum ColorEnum
    {
        Red,
        Green,
        AllForOne
    }
}
