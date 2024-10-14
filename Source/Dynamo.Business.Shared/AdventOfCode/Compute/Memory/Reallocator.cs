using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class Reallocator
    {
        public BlockList Blocks { get; private set; }

        // Properties to store the loop index and loop size.
        public int LoopIndex { get; private set; }
        public int LoopSize { get; private set; }

        public Reallocator(string blocks)
        {
            Blocks = new BlockList();
            foreach (var block in blocks.Split('\t'))
            {
                Blocks.Add(int.Parse(block));
            }

            Reallocate();
        }

        // Performs the reallocation and stores loop index and size.
        private void Reallocate()
        {
            var seenConfigurations = new Dictionary<string, int>();
            seenConfigurations[Blocks.ToSnapshot()] = 0;

            LoopIndex = 0;

            while (true)
            {
                LoopIndex++;
                Blocks.Reallocate();
                string snapshot = Blocks.ToSnapshot();

                if (seenConfigurations.TryGetValue(snapshot, out int firstSeenIndex))
                {
                    LoopSize = LoopIndex - firstSeenIndex; // Calculate loop size.
                    break;
                }

                seenConfigurations[snapshot] = LoopIndex;
            }
        }
    }
}
