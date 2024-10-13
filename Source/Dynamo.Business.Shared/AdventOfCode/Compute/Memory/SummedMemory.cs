using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class SummedMemory : SpiralMemory
    {
        protected override bool ShouldExit(int value, int limit) => value > limit;

        protected override int GetNextValue(int x, int y, int currentValue)
        {
            return GetSummedValue(x, y);
        }

        private int GetSummedValue(int x, int y)
        {
            int sum = 0;
            var neighbors = new (int x, int y)[]
            {
            (x - 1, y + 1), (x, y + 1), (x + 1, y + 1),
            (x - 1, y),             /*(x, y)*/          (x + 1, y),
            (x - 1, y - 1), (x, y - 1), (x + 1, y - 1)
            };

            foreach (var (nx, ny) in neighbors)
            {
                sum += Values.GetValueOrDefault((nx, ny), 0);
            }

            return sum;
        }
    }
}
