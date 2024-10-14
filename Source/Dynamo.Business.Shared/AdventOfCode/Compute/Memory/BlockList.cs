using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{

    public class BlockList
    {
        private readonly List<int> _blocks = new List<int>();

        public IReadOnlyList<int> Blocks => _blocks;

        public void Add(int blockValue) => _blocks.Add(blockValue);

        public void Reallocate()
        {
            if (_blocks.Count == 0) return;

            int maxBlock = _blocks.Max();
            int indexOf = _blocks.IndexOf(maxBlock);
            _blocks[indexOf] = 0;

            for (int i = 1; i <= maxBlock; i++)
            {
                int currentBlock = (indexOf + i) % _blocks.Count;
                _blocks[currentBlock]++;
            }
        }

        public string ToSnapshot()
        {
            return string.Join(",", _blocks); // Convert the block list to a string snapshot.
        }
    }
}
