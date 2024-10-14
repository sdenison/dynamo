using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class Reallocator
    {
        public BlockList Blocks { get; private set; }

        public Reallocator(string blocks)
        {
            Blocks = new BlockList();
            var blockIndex = 0;
            foreach (var block in blocks.Split('\t'))
            {
                Blocks.Add(int.Parse(block));
                blockIndex++;
            }
        }

        public int Reallocate()
        {
            var blockDictionary = new Dictionary<int, BlockList>();
            blockDictionary.Add(Blocks.GetHashCode(), Blocks);
            var loopIndex = 0;
            do
            {
                loopIndex++;
                Blocks.Reallocate();
                if (blockDictionary.Keys.Contains(Blocks.GetHashCode()))
                {
                    return loopIndex;
                }
                blockDictionary.Add(Blocks.GetHashCode(), Blocks);
            } while (true);
        }

        public int ReallocateFiniteLoopSize()
        {
            var blockDictionary = new Dictionary<int, BlockList>();
            blockDictionary.Add(Blocks.GetHashCode(), Blocks);
            var loopIndex = 0;
            do
            {
                loopIndex++;
                Blocks.Reallocate();
                if (blockDictionary.Keys.Contains(Blocks.GetHashCode()))
                {
                    return loopIndex - blockDictionary.Keys.ToList().IndexOf(Blocks.GetHashCode());
                }
                blockDictionary.Add(Blocks.GetHashCode(), Blocks);
            } while (true);
        }
    }
}
