using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {
        public static List<Point> GetIncrementedMemory(int memorySquare)
        {
            var currentPoint = new Point(0, 0, 1);
            var spiralWidth = 0;
            var memory = new List<Point>();

            memory.Add(currentPoint);

            while (true)
            {
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (direction == Direction.Right)
                    {
                        spiralWidth++;
                        for (var x = currentPoint.X; x < spiralWidth; x++)
                        {
                            currentPoint.X++;
                            currentPoint.Value++;
                            memory.Add(new Point(currentPoint.X, currentPoint.Y, currentPoint.Value));
                            if (currentPoint.Value == memorySquare)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Up)
                    {
                        for (var y = currentPoint.Y; y < spiralWidth; y++)
                        {
                            currentPoint.Y++;
                            currentPoint.Value++;
                            memory.Add(new Point(currentPoint.X, currentPoint.Y, currentPoint.Value));
                            if (currentPoint.Value == memorySquare)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Left)
                    {
                        for (var x = currentPoint.X; x > spiralWidth * -1; x--)
                        {
                            currentPoint.X--;
                            currentPoint.Value++;
                            memory.Add(new Point(currentPoint.X, currentPoint.Y, currentPoint.Value));
                            if (currentPoint.Value == memorySquare)
                            {
                                return memory;
                            }
                        }
                    }
                    if (direction == Direction.Down)
                    {
                        for (var y = currentPoint.Y; y > spiralWidth * -1; y--)
                        {
                            currentPoint.Y--;
                            currentPoint.Value++;
                            memory.Add(new Point(currentPoint.X, currentPoint.Y, currentPoint.Value));
                            if (currentPoint.Value == memorySquare)
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
            var currentPoint = new Point(0, 0, 1);
            var spiralWidth = 0;
            var memory = new List<Point>();

            memory.Add(currentPoint);

            var value = 0;
            var currentX = 0;
            var currentY = 0;

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
