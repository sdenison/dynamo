namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Cart
    {
        public TrackSection TrackSection { get; set; }
        public Point Point => TrackSection.Point;
        public Rotation Rotation { get; set; }
        public NextMove NextMove { get; set; }

        public Cart(TrackSection trackSection, Rotation rotation)
        {
            TrackSection = trackSection;
            Rotation = rotation;
            NextMove = NextMove.GoLeft;
        }

        public void MoveBy1()
        {
            TrackSection nextSection = null;
            if (Rotation == Rotation.Clockwise)
                nextSection = TrackSection.Next;
            else
                nextSection = TrackSection.Previous;

            if (nextSection.Type != TrackSectionType.Intersection)
                TrackSection = nextSection;
            else
            {
                if (NextMove == NextMove.GoStraight)
                {
                    if (Rotation == Rotation.Clockwise)
                        TrackSection = TrackSection.Next;
                    else
                        TrackSection = TrackSection.Previous;
                    NextMove = NextMove.GoRight;
                    return;
                }

                TrackSection interSectingSection = null;
                if (Rotation == Rotation.Clockwise)
                    interSectingSection = TrackSection.Next.IntersectingTrackSection;
                else
                    interSectingSection = TrackSection.Previous.IntersectingTrackSection;

                if ((Rotation == Rotation.Clockwise && interSectingSection.Side == Side.Top && TrackSection.Side == Side.Right) ||
                    (Rotation == Rotation.Clockwise && interSectingSection.Side == Side.Bottom && TrackSection.Side == Side.Left) ||
                    (Rotation == Rotation.CounterClockwise && interSectingSection.Side == Side.Bottom && TrackSection.Side == Side.Left) ||
                    (Rotation == Rotation.CounterClockwise && interSectingSection.Side == Side.Left && TrackSection.Side == Side.Top))
                {
                    switch (NextMove)
                    {
                        case NextMove.GoLeft:
                            Rotation = Rotation.Clockwise;
                            NextMove = NextMove.GoStraight;
                            break;
                        case NextMove.GoRight:
                            Rotation = Rotation.CounterClockwise;
                            NextMove = NextMove.GoLeft;
                            break;
                    }
                }
                else
                {
                    switch (NextMove)
                    {
                        case NextMove.GoLeft:
                            Rotation = Rotation.CounterClockwise;
                            NextMove = NextMove.GoStraight;
                            break;
                        case NextMove.GoRight:
                            Rotation = Rotation.Clockwise;
                            NextMove = NextMove.GoLeft;
                            break;
                    }
                }
                TrackSection = interSectingSection;
            }
        }
    }

    public enum Rotation
    {
        Clockwise,
        CounterClockwise
    }

    public enum NextMove
    {
        GoLeft,
        GoStraight,
        GoRight
    }
}
