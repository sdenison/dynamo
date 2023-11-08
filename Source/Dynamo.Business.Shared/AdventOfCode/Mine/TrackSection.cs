namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class TrackSection
    {
        public Point Point { get; }
        public TrackSectionType Type { get; }
        public Track Track { get; }
        //public Track IntersectionTrack { get; set; }
        public TrackSection IntersectingTrackSection { get; set; }
        public TrackSection Next { get; set; }
        public TrackSection Previous { get; set; }
        public Side Side { get; set; }

        public TrackSection(Point point, TrackSectionType trackSectionType, Track track, Side side)
        {
            Point = point;
            Type = trackSectionType;
            Track = track;
            Side = side;
        }

        public TrackSection(Point point, TrackSectionType trackSectionType, Track track, TrackSection previous, Side side)
        {
            Point = point;
            Type = trackSectionType;
            Track = track;
            Previous = previous;
            previous.Next = this;
            Side = side;
        }
    }
}
