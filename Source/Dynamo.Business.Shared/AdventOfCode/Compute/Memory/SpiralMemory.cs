using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {
        public static List<Point> GetIncrementedMemory(int memorySquare)
        {
            var memory = new List<Point>();
            var values = new Dictionary<(int x, int y), int>();
            AddPoint(memory, values, 0, 0, 1);

            Direction currentDirection = Direction.Right;
            int currentX = 0, currentY = 0, value = 1, spiralSize = 1;

            while (value < memorySquare)
            {
                (currentX, currentY, value) = TraverseSpiral(
                    currentDirection, spiralSize, currentX, currentY, value, memorySquare, memory, values, false);
                if (value == memorySquare) return memory;

                if (currentDirection == Direction.Up || currentDirection == Direction.Down) spiralSize++;
                currentDirection = GetNextDirection(currentDirection);
            }

            return memory;
        }

        public static List<Point> GetSummedMemory(int maxValue)
        {
            var memory = new List<Point>();
            var values = new Dictionary<(int x, int y), int>();
            AddPoint(memory, values, 0, 0, 1);

            Direction currentDirection = Direction.Right;
            int currentX = 0, currentY = 0, value = 1, spiralSize = 1;

            while (true)
            {
                (currentX, currentY, value) = TraverseSpiral(
                    currentDirection, spiralSize, currentX, currentY, value, maxValue, memory, values, true);
                if (value > maxValue) return memory;

                if (currentDirection == Direction.Up || currentDirection == Direction.Down) spiralSize++;
                currentDirection = GetNextDirection(currentDirection);
            }
        }

        private static (int, int, int) TraverseSpiral(
            Direction direction, int steps, int currentX, int currentY, int value, int limit,
            List<Point> memory, Dictionary<(int x, int y), int> values, bool useSummedValues)
        {
            var (dx, dy) = GetDirectionVector(direction);

            for (int i = 0; i < steps; i++)
            {
                currentX += dx;
                currentY += dy;
                value = useSummedValues ? GetSummedValue(values, currentX, currentY) : value + 1;

                AddPoint(memory, values, currentX, currentY, value);

                // Condition check based on the use case
                if (useSummedValues && value > limit) return (currentX, currentY, value);
                if (!useSummedValues && value == limit) return (currentX, currentY, value);
            }

            return (currentX, currentY, value);
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

        private static int GetSummedValue(Dictionary<(int x, int y), int> values, int x, int y)
        {
            int sum = 0;
            var neighbors = new (int x, int y)[]
            {
            (x - 1, y + 1), (x, y + 1), (x + 1, y + 1),
            (x - 1, y), /*(x, y)*/ (x + 1, y),
            (x - 1, y - 1), (x, y - 1), (x + 1, y - 1)
            };

            foreach (var (nx, ny) in neighbors)
            {
                sum += values.GetValueOrDefault((nx, ny), 0);
            }

            return sum;
        }

        private static void AddPoint(
            List<Point> memory, Dictionary<(int x, int y), int> values, int x, int y, int value)
        {
            var point = new Point(x, y, value);
            memory.Add(point);
            values[(x, y)] = value;
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
