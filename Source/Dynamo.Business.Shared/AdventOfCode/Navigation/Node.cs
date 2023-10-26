using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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
