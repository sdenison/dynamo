namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class TrackSection
    {
        public Point Point { get; }
        public TrackSectionType Type { get; }
        public Track Track { get; }
        public Track IntersectionTrack { get; set; }
        public TrackSection Next { get; set; }
        public TrackSection Previous { get; set; }

        public TrackSection(Point point, TrackSectionType trackSectionType, Track track)
        {
            Point = point;
            Type = trackSectionType;
            Track = track;
        }

        public TrackSection(Point point, TrackSectionType trackSectionType, Track track, TrackSection previous)
        {
            Point = point;
            Type = trackSectionType;
            Track = track;
            Previous = previous;
            previous.Next = this;
        }
    }
}
