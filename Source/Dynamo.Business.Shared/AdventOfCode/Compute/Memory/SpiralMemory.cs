using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {
        public static List<Point> GetIncrementedMemory(int memorySquare)
        {
            var memory = new List<Point>();
            int currentX = 0, currentY = 0, value = 1, spiralSize = 1;
            memory.Add(new Point(currentX, currentY, value));

            Direction currentDirection = Direction.Right;

            while (value < memorySquare)
            {
                var (dx, dy) = GetDxDy(currentDirection);

                for (int i = 0; i < spiralSize; i++)
                {
                    currentX += dx;
                    currentY += dy;
                    value++;
                    memory.Add(new Point(currentX, currentY, value));
                    if (value == memorySquare) return memory;
                }

                // Increment the spiral size if we were just going Up or Down
                if (currentDirection == Direction.Up || currentDirection == Direction.Down)
                {
                    spiralSize++;
                }

                // Move to the next direction in the sequence
                currentDirection = GetNextDirection(currentDirection);
            }

            return memory;
        }

        public static List<Point> GetSummedMemory(int maxValue)
        {
            var memory = new List<Point>();
            int currentX = 0, currentY = 0, value = 1, spiralSize = 1;
            memory.Add(new Point(currentX, currentY, value));

            Direction currentDirection = Direction.Right;

            while (value <= maxValue)
            {
                var (dx, dy) = GetDxDy(currentDirection);

                for (int i = 0; i < spiralSize; i++)
                {
                    currentX += dx;
                    currentY += dy;
                    value = GetSummedValues(memory, currentX, currentY);
                    memory.Add(new Point(currentX, currentY, value));
                    if (value > maxValue) return memory;
                }

                // Increment the spiral size if we were just going Up or Down
                if (currentDirection == Direction.Up || currentDirection == Direction.Down)
                {
                    spiralSize++;
                }

                // Move to the next direction in the sequence
                currentDirection = GetNextDirection(currentDirection);
            }

            return memory;
        }

        private static (int dx, int dy) GetDxDy(Direction direction)
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

        private static int GetSummedValues(List<Point> memory, int x, int y)
        {
            int sum = 0;
            sum += memory.SingleOrDefault(point => point.X == x - 1 && point.Y == y + 1)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x && point.Y == y + 1)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x + 1 && point.Y == y + 1)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x - 1 && point.Y == y)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x + 1 && point.Y == y)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x - 1 && point.Y == y - 1)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x && point.Y == y - 1)?.Value ?? 0;
            sum += memory.SingleOrDefault(point => point.X == x + 1 && point.Y == y - 1)?.Value ?? 0;

            return sum;
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
