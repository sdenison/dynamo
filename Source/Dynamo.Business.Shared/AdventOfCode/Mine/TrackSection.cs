namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class TrackSection
    {
        public Point Point { get; }
        public TrackSectionType Type { get; }

        public TrackSection(Point point, TrackSectionType trackSectionType)
        {
            Point = point;
            Type = trackSectionType;
        }
    }
}
