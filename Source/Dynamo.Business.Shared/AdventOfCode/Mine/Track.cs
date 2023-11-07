using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Track
    {
        public List<TrackSection> Sections { get; }
        public List<Cart> Carts { get; }

        public Track(Point startPoint, Point[,] allPoints)
        {
            var x = startPoint.X;
            var y = startPoint.Y;

            Sections = new List<TrackSection>();
            Carts = new List<Cart>();
            Sections.Add(new TrackSection(startPoint, TrackSectionType.TopLeft));
            var hasMorePoints = true;
            var currentPoint = startPoint;
            var forwardSlashCount = 0;
            var backSlashCount = 0;
            var currentDirection = CurrentDirection.Right;
            while (hasMorePoints)
            {
                if (currentPoint.PointChar == '/')
                {
                    if (forwardSlashCount == 0)
                    {
                        Sections.Add(new TrackSection(currentPoint, TrackSectionType.TopLeft));
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                        currentDirection = CurrentDirection.Right;
                    }
                    else
                    {
                        Sections.Add(new TrackSection(currentPoint, TrackSectionType.LowerRight));
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                        currentDirection = CurrentDirection.Left;
                    }
                    forwardSlashCount++;
                }
                if (currentPoint.PointChar == '\\')
                {
                    if (backSlashCount == 0)
                    {
                        Sections.Add(new TrackSection(currentPoint, TrackSectionType.TopRight));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                        currentDirection = CurrentDirection.Down;
                    }
                    else
                    {
                        Sections.Add(new TrackSection(currentPoint, TrackSectionType.LowerLeft));
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                        currentDirection = CurrentDirection.Up;
                    }
                    backSlashCount++;
                }
                if (currentPoint.PointChar == '-')
                {
                    Sections.Add(new TrackSection(currentPoint, TrackSectionType.Horizontal));
                    if (currentDirection == CurrentDirection.Right)
                        currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                    else
                        currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                }
                if (currentPoint.PointChar == '|')
                {
                    Sections.Add(new TrackSection(currentPoint, TrackSectionType.Horizontal));
                    if (currentDirection == CurrentDirection.Down)
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                    else
                        currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                }
                if (currentPoint.PointChar == '+')
                {
                    Sections.Add(new TrackSection(currentPoint, TrackSectionType.Intersection));
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
                if (currentPoint.PointChar == '>')
                {
                    var newSection = new TrackSection(currentPoint, TrackSectionType.Horizontal);
                    Carts.Add(new Cart(newSection, CurrentDirection.Right));
                    Sections.Add(newSection);
                    currentPoint = allPoints[currentPoint.X + 1, currentPoint.Y];
                }
                if (currentPoint.PointChar == '<')
                {
                    var newSection = new TrackSection(currentPoint, TrackSectionType.Horizontal);
                    Carts.Add(new Cart(newSection, CurrentDirection.Left));
                    Sections.Add(newSection);
                    currentPoint = allPoints[currentPoint.X - 1, currentPoint.Y];
                }
                if (currentPoint.PointChar == '^')
                {
                    var newSection = new TrackSection(currentPoint, TrackSectionType.Vertical);
                    Carts.Add(new Cart(newSection, CurrentDirection.Up));
                    Sections.Add(newSection);
                    currentPoint = allPoints[currentPoint.X, currentPoint.Y - 1];
                }
                if (currentPoint.PointChar == 'v')
                {
                    var newSection = new TrackSection(currentPoint, TrackSectionType.Vertical);
                    Carts.Add(new Cart(newSection, CurrentDirection.Down));
                    Sections.Add(newSection);
                    currentPoint = allPoints[currentPoint.X, currentPoint.Y + 1];
                }
                if (currentPoint == startPoint)
                    hasMorePoints = false;
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
}
