using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Track
    {
        public List<TrackSection> Sections { get; }
        public List<Cart> Carts { get; }
        public Dictionary<Point, Track> Intersections { get; set; }

        public Track(Point startPoint, Point[,] allPoints)
        {
            var x = startPoint.X;
            var y = startPoint.Y;

            Sections = new List<TrackSection>();
            Carts = new List<Cart>();
            Intersections = new Dictionary<Point, Track>();
            var hasMorePoints = true;
            var currentPoint = startPoint;
            var forwardSlashCount = 1;
            var backSlashCount = 0;
            var currentDirection = CurrentDirection.Right;

            var side = Side.Top;
            var startingSection = new TrackSection(currentPoint, TrackSectionType.TopLeft, this, side);
            var currentSection = startingSection;
            var previousSection = currentSection;

            Sections.Add(currentSection);
            currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
            while (hasMorePoints)
            {
                if (currentPoint.PointChar == '/')
                {
                    if (forwardSlashCount == 0)
                    {
                        currentSection = new TrackSection(currentPoint, TrackSectionType.TopLeft, this, previousSection, side);
                        Sections.Add(currentSection);
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                        currentDirection = CurrentDirection.Right;
                        side = Side.Top;
                    }
                    else
                    {
                        currentSection = new TrackSection(currentPoint, TrackSectionType.LowerRight, this, previousSection, side);
                        Sections.Add(currentSection);
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                        currentDirection = CurrentDirection.Left;
                        side = Side.Bottom;
                    }
                    forwardSlashCount++;
                }
                else if (currentPoint.PointChar == '\\')
                {
                    if (backSlashCount == 0)
                    {
                        currentSection = new TrackSection(currentPoint, TrackSectionType.TopRight, this, previousSection, side);
                        Sections.Add(currentSection);
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                        currentDirection = CurrentDirection.Down;
                        side = Side.Right;
                    }
                    else
                    {
                        currentSection = new TrackSection(currentPoint, TrackSectionType.LowerLeft, this,
                            previousSection, side);
                        Sections.Add(currentSection);
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                        currentDirection = CurrentDirection.Up;
                        side = Side.Left;
                    }
                    backSlashCount++;
                }
                else if (currentPoint.PointChar == '-')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Horizontal, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Right)
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                    else
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                }
                else if (currentPoint.PointChar == '|')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Vertical, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Down)
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                    else
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                }
                else if (currentPoint.PointChar == '+')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Intersection, this, previousSection, side);
                    Sections.Add(currentSection);
                    switch (currentDirection)
                    {
                        case CurrentDirection.Left:
                            currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                            break;
                        case CurrentDirection.Right:
                            currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                            break;
                        case CurrentDirection.Down:
                            currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                            break;
                        case CurrentDirection.Up:
                            currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                            break;
                    }
                }
                else if (currentPoint.PointChar == '>')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Horizontal, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Right)
                    {
                        Carts.Add(new Cart(currentSection, Rotation.Clockwise));
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                    }
                    if (currentDirection == CurrentDirection.Left)
                    {
                        Carts.Add(new Cart(currentSection, Rotation.CounterClockwise));
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                    }
                }
                else if (currentPoint.PointChar == '<')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Horizontal, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Right)
                    {
                        Carts.Add(new Cart(currentSection, Rotation.CounterClockwise));
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                    }
                    else
                    {
                        Carts.Add(new Cart(currentSection, Rotation.Clockwise));
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                    }
                }
                else if (currentPoint.PointChar == '^')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Vertical, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Down)
                    {
                        Carts.Add(new Cart(currentSection, Rotation.CounterClockwise));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                    }
                    else
                    {
                        Carts.Add(new Cart(currentSection, Rotation.Clockwise));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                    }
                }
                else if (currentPoint.PointChar == 'v')
                {
                    currentSection = new TrackSection(currentPoint, TrackSectionType.Vertical, this, previousSection, side);
                    Sections.Add(currentSection);
                    if (currentDirection == CurrentDirection.Down)
                    {
                        Carts.Add(new Cart(currentSection, Rotation.Clockwise));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                    }
                    else
                    {
                        Carts.Add(new Cart(currentSection, Rotation.CounterClockwise));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                    }
                }
                previousSection = currentSection;
                if (currentPoint == startPoint)
                {
                    startingSection.Previous = currentSection;
                    currentSection.Next = startingSection;
                    hasMorePoints = false;
                }
            }
        }
    }

    public enum CurrentDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum Side
    {
        Top,
        Right,
        Bottom,
        Left
    }
}
