using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SpiralMemory
    {
        public Point FinalPoint { get; private set; }

        public SpiralMemory(int memorySquare)
        {
            var currentPoint = new Point(0, 0, 1);
            var spiralWidth = 0;

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
                            if (currentPoint.Value == memorySquare)
                            {
                                FinalPoint = currentPoint;
                                return;
                            }
                        }
                    }
                    if (direction == Direction.Up)
                    {
                        for (var y = currentPoint.Y; y < spiralWidth; y++)
                        {
                            currentPoint.Y++;
                            currentPoint.Value++;
                            if (currentPoint.Value == memorySquare)
                            {
                                FinalPoint = currentPoint;
                                return;
                            }
                        }
                    }
                    if (direction == Direction.Left)
                    {
                        for (var x = currentPoint.X; x > spiralWidth * -1; x--)
                        {
                            currentPoint.X--;
                            currentPoint.Value++;
                            if (currentPoint.Value == memorySquare)
                            {
                                FinalPoint = currentPoint;
                                return;
                            }
                        }
                    }
                    if (direction == Direction.Down)
                    {
                        for (var y = currentPoint.Y; y > spiralWidth * -1; y--)
                        {
                            currentPoint.Y--;
                            currentPoint.Value++;
                            if (currentPoint.Value == memorySquare)
                            {
                                FinalPoint = currentPoint;
                                return;
                            }
                        }
                    }
                }
            }
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
