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

        public void Step()
        {
            TrackSection nextSection = null;
            if (Rotation == Rotation.Clockwise)
                nextSection = TrackSection.Next;
            else
                nextSection = TrackSection.Previous;

            if (nextSection.Type != TrackSectionType.Intersection)
            {
                TrackSection = nextSection;
                return;
            }

            //Everything else is logic for the intersections
            if (NextMove == NextMove.GoStraight)
            {
                if (Rotation == Rotation.Clockwise)
                    TrackSection = TrackSection.Next;
                else
                    TrackSection = TrackSection.Previous;
                NextMove = NextMove.GoRight;
                return;
            }

            var interSectingSection = nextSection.IntersectingTrackSection;

            if ((TrackSection.Side == Side.Top && interSectingSection.Side == Side.Left && Rotation == Rotation.Clockwise) ||
                (TrackSection.Side == Side.Top && interSectingSection.Side == Side.Right && Rotation == Rotation.CounterClockwise) ||
                (TrackSection.Side == Side.Bottom && interSectingSection.Side == Side.Left && Rotation == Rotation.CounterClockwise) ||
                (TrackSection.Side == Side.Bottom && interSectingSection.Side == Side.Right && Rotation == Rotation.Clockwise) ||
                (TrackSection.Side == Side.Left && interSectingSection.Side == Side.Top && Rotation == Rotation.CounterClockwise) ||
                (TrackSection.Side == Side.Left && interSectingSection.Side == Side.Bottom && Rotation == Rotation.Clockwise) ||
                (TrackSection.Side == Side.Right && interSectingSection.Side == Side.Top && Rotation == Rotation.Clockwise) ||
                (TrackSection.Side == Side.Right && interSectingSection.Side == Side.Bottom && Rotation == Rotation.CounterClockwise)
                )
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
