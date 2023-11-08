namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Cart
    {
        public TrackSection TrackSection { get; set; }
        public Point Point => TrackSection.Point;
        public Rotation Rotation { get; set; }

        public Cart(TrackSection trackSection, Rotation rotation)
        {
            TrackSection = trackSection;
            Rotation = rotation;
        }
    }

    public enum Rotation
    {
        Clockwise,
        CounterClockwise
    }
}
