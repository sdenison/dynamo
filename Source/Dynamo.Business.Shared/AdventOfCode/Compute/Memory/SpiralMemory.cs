using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public abstract class SpiralMemory
    {
        public List<Point> Memory { get; private set; }
        protected Dictionary<(int x, int y), int> Values { get; private set; }

        public List<Point> Generate(int limit)
        {
            InitializeMemory();
            Direction currentDirection = Direction.Right;
            int currentX = 0, currentY = 0, value = 1, spiralSize = 1;

            while (!ShouldExit(value, limit))
            {
                (currentX, currentY, value) = TraverseSpiral(
                    currentDirection, spiralSize, currentX, currentY, value, limit);

                if (currentDirection == Direction.Up || currentDirection == Direction.Down)
                {
                    spiralSize++;
                }

                currentDirection = GetNextDirection(currentDirection);
            }

            return Memory;
        }

        protected abstract bool ShouldExit(int value, int limit);

        private void InitializeMemory()
        {
            Memory = new List<Point>();
            Values = new Dictionary<(int x, int y), int>();
            AddPoint(0, 0, 1);
        }

        private (int, int, int) TraverseSpiral(
            Direction direction, int steps, int currentX, int currentY, int value, int limit)
        {
            var (dx, dy) = GetDirectionVector(direction);

            for (int i = 0; i < steps; i++)
            {
                currentX += dx;
                currentY += dy;
                value = GetNextValue(currentX, currentY, value);

                AddPoint(currentX, currentY, value);
                if (ShouldExit(value, limit)) return (currentX, currentY, value);
            }

            return (currentX, currentY, value);
        }

        protected virtual int GetNextValue(int x, int y, int currentValue) => currentValue + 1;

        private void AddPoint(int x, int y, int value)
        {
            var point = new Point(x, y, value);
            Memory.Add(point);
            Values[(x, y)] = value;
        }

        private static (int dx, int dy) GetDirectionVector(Direction direction)
        {
            return direction switch
            {
                Direction.Right => (1, 0),
                Direction.Up => (0, 1),
                Direction.Left => (-1, 0),
                Direction.Down => (0, -1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid direction: {direction}")
            };
        }

        private static Direction GetNextDirection(Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.Right => Direction.Up,
                Direction.Up => Direction.Left,
                Direction.Left => Direction.Down,
                Direction.Down => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirection), $"Invalid direction: {currentDirection}")
            };
        }
    }

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }
}
