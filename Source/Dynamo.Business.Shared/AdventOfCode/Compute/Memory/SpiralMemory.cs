using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {
        public static List<Point> GetIncrementedMemory(int memorySquare)
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
                            value++;
                            memory.Add(new Point(currentX, currentY, value));
                            if (value == memorySquare)
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
                            value++;
                            memory.Add(new Point(currentX, currentY, value));
                            if (value == memorySquare)
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
                            value++;
                            memory.Add(new Point(currentX, currentY, value));
                            if (value == memorySquare)
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
                            value++;
                            memory.Add(new Point(currentX, currentY, value));
                            if (value == memorySquare)
                            {
                                return memory;
                            }
                        }
                    }
                }
            }
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
