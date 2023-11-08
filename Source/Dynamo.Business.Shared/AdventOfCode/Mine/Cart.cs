using System.Transactions;

namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Cart
    {
        public TrackSection TrackSection { get; set; }
        public Point Point => TrackSection.Point;
        public Rotation Rotation { get; set; }
        public int IntersectionInt { get; set; }

        public Cart(TrackSection trackSection, Rotation rotation)
        {
            TrackSection = trackSection;
            Rotation = rotation;
            IntersectionInt = 0;
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
                if (IntersectionInt == 1)
                {
                    if (Rotation == Rotation.Clockwise)
                        TrackSection = TrackSection.Next;
                    else
                        TrackSection = TrackSection.Previous;
                    IntersectionInt++;
                    return;
                    //return TrackSection;
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
                    //We're hitting the left of the intersection
                    switch (IntersectionInt)
                    {
                        case 0:
                            Rotation = Rotation.Clockwise;
                            TrackSection = interSectingSection;
                            IntersectionInt++;
                            break;
                        case 2:
                            Rotation = Rotation.CounterClockwise;
                            TrackSection = interSectingSection;
                            IntersectionInt = 0;
                            break;
                    }
                }
                else
                {
                    switch (IntersectionInt)
                    {
                        case 0:
                            Rotation = Rotation.CounterClockwise;
                            TrackSection = interSectingSection;
                            IntersectionInt++;
                            break;
                        case 2:
                            Rotation = Rotation.Clockwise;
                            TrackSection = interSectingSection;
                            IntersectionInt = 0;
                            break;
                    }
                }

                //if ((Rotation == Rotation.Clockwise && interSectingSection.Side == Side.Bottom && TrackSection.Side == Side.Right) ||
                //    (Rotation == Rotation.Clockwise && interSectingSection.Side == Side.Left && TrackSection.Side == Side.Top) ||
                //    (Rotation == Rotation.CounterClockwise && interSectingSection.Side == Side.Bottom && TrackSection.Side == Side.Left) ||
                //    (Rotation == Rotation.CounterClockwise && interSectingSection.Side == Side.))
                //{
                //    //We're hitting the left of the intersection
                //    switch (IntersectionInt)
                //    {
                //        case 0:
                //            Rotation = Rotation.CounterClockwise;
                //            TrackSection = interSectingSection;
                //            IntersectionInt++;
                //            break;
                //        case 2:
                //            Rotation = Rotation.Clockwise;
                //            TrackSection = interSectingSection;
                //            IntersectionInt = 0;
                //            break;
                //    }
                //}
                //}
            }
            // return TrackSection;
        }
    }

    public enum Rotation
    {
        Clockwise,
        CounterClockwise
    }
}
