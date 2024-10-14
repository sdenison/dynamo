using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class BlockList
    {
        public List<int> Blocks { get; set; } = new List<int>();

        public override int GetHashCode()
        {
            var hashCode = new StringBuilder();
            foreach (var block in Blocks)
            {
                hashCode.Append(block.GetHashCode());
                hashCode.Append(" ");
            }
            return hashCode.ToString().GetHashCode();
        }

        public void Add(int blockValue)
        {
            Blocks.Add(blockValue);
        }

        public void Reallocate()
        {
            var maxBlock = 0;
            foreach (var block in Blocks)
            {
                if (block > maxBlock)
                    maxBlock = block;
            }
            var indexOf = Blocks.IndexOf(maxBlock);
            var amountToRedistribute = maxBlock;
            Blocks[indexOf] = 0;
            for (var i = 1; i <= amountToRedistribute; i++)
            {
                var currentBlock = (indexOf + i) % Blocks.Count;
                Blocks[currentBlock]++;
            }
        }
    }
}
