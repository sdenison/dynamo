using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {

        public static List<Point> GetMemory(int valueToSearchFor, Func<int, int> increment, Func<int, int> comparer)
        {
            var spiralWidth = 0;
            var memory = new List<Point>();
            var currentX = 0;
            var currentY = 0;
            var value = 1;

            memory.Add(new Point(currentX, currentY, value));
            return memory;
        }

        public static List<Point> GetIncrementedMemory(int memorySquare)
        {
            var memory = new List<Point>();
            int currentX = 0, currentY = 0, value = 1, spiralWidth = 1;
            memory.Add(new Point(currentX, currentY, value));
            var directions = new (int x, int y)[]
            {
                (1, 0),  // Right
                (0, 1),  // Up
                (-1, 0), // Left
                (0, -1)  // Down
            };

            var currentDirection = 0;

            while (value < memorySquare)
            {
                var (dx, dy) = directions[currentDirection % 4];

                for (int i = 0; i < spiralWidth; i++)
                {
                    currentX += dx;
                    currentY += dy;
                    value++;
                    memory.Add(new Point(currentX, currentY, value));
                    if (value == memorySquare) return memory;
                }

                // Increment the width if we were just going up or down
                if (currentDirection % 4 == 1 || currentDirection % 4 == 3)
                {
                    spiralWidth++;
                }
                currentDirection++;
            }

            return memory;
        }

        public static List<Point> GetSummedMemory(int maxMemoryValue)
        {
            var spiralWidth = 0;
            var memory = new List<Point>();
            var currentX = 0;
            var currentY = 0;
            var value = 1;

            memory.Add(new Point(currentX, currentY, value));

            while (true)
            {
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (direction == Direction.Right)
                    {
                        spiralWidth++;
                        for (var x = currentX; x < spiralWidth; x++)
                        {
                            currentX++;
                            value = GetSummedValues(memory, currentX, currentY);
                            memory.Add(new Point(currentX, currentY, value));
                            if (value > maxMemoryValue)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Up)
                    {
                        for (var y = currentY; y < spiralWidth; y++)
                        {
                            currentY++;
                            value = GetSummedValues(memory, currentX, currentY);
                            memory.Add(new Point(currentX, currentY, value));
                            if (value > maxMemoryValue)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Left)
                    {
                        for (var x = currentX; x > spiralWidth * -1; x--)
                        {
                            currentX--;
                            value = GetSummedValues(memory, currentX, currentY);
                            memory.Add(new Point(currentX, currentY, value));
                            if (value > maxMemoryValue)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Down)
                    {
                        for (var y = currentY; y > spiralWidth * -1; y--)
                        {
                            currentY--;
                            value = GetSummedValues(memory, currentX, currentY);
                            memory.Add(new Point(currentX, currentY, value));
                            if (value > maxMemoryValue)
                            {
                                return memory;
                            }
                        }
                    }
                }
            }
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
