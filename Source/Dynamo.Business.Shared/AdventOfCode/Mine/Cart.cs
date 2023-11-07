namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Cart
    {
        public TrackSection TrackSection { get; set; }
        public CurrentDirection Direction { get; set; }

        public Cart(TrackSection trackSection, CurrentDirection direction)
        {
            TrackSection = trackSection;
            Direction = direction;
        }
    }
}
