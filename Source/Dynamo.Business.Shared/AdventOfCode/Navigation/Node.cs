using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class Node
    {
        public List<Node> ChildNodes { get; } = new List<Node>();
        public List<int> Metadata { get; } = new List<int>();
        public int Size
        {
            get
            {
                return 2 + ChildNodes.Sum(x => x.Size) + Metadata.Count;
            }
        }
        public long SumAllMetadata
        {
            get
            {
                long metaDataSum = 0;
                foreach (var childNode in ChildNodes)
                    metaDataSum += childNode.SumAllMetadata;
                return Metadata.Sum() + metaDataSum;
            }
        }

        public long Value
        {
            get
            {
                if (ChildNodes.Count == 0)
                    return Metadata.Sum();
                long value = 0;
                foreach (var metaData in Metadata)
                {
                    if (metaData == 0)
                        continue;
                    if (metaData > ChildNodes.Count)
                        continue;
                    value += ChildNodes[metaData - 1].Value;
                }
                return value;
            }
        }

        public Node(string nodeDefinitionString) : this(nodeDefinitionString.Split(' ').Select(x => int.Parse(x)).ToArray())
        {
        }

        public Node(int[] nodeDefinition)
        {
            if (nodeDefinition[0] == 0)
                for (var i = 0; i < nodeDefinition[1]; i++)
                    Metadata.Add(nodeDefinition[2 + i]);
            else
            {
                for (var i = 0; i < nodeDefinition[0]; i++)
                {
                    var childDefinition = nodeDefinition.Skip(2 + ChildNodes.Sum(x => x.Size)).ToArray();
                    ChildNodes.Add(new Node(childDefinition));
                }
                for (var i = 0; i < nodeDefinition[1]; i++)
                {

                    Metadata.Add(nodeDefinition[2 + ChildNodes.Sum(x => x.Size) + i]);
                }
            }
        }
    }
}
