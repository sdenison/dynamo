using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Navigation
{
    public class Node
    {
        public List<Node> ChildNodes { get; } = new List<Node>();
        public List<int> Metadata { get; } = new List<int>();

        public Node(int[] nodeDefinition)
        {
            if (nodeDefinition[0] == 0)
                for (var i = 0; i < nodeDefinition[1]; i++)
                    Metadata.Add(nodeDefinition[2 + i]);
        }
    }
}
