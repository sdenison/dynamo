using System.Linq;
using Dynamo.Business.Shared.AdventOfCode.Navigation;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Navigation
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void Can_create_a_node_with_no_children()
        {
            var nodeDefinition = new[] { 0, 1, 99 };
            var node = new Node(nodeDefinition);
            Assert.AreEqual(1, node.Metadata.Count);
            Assert.AreEqual(99, node.Metadata[0]);
            Assert.AreEqual(99, node.Value);
        }

        [Test]
        public void Can_create_a_node_with_one_child()
        {
            var nodeDefinition = new[] { 1, 1, 0, 1, 99, 2 };
            var node = new Node(nodeDefinition);
            Assert.AreEqual(2, node.Metadata[0]);
            Assert.AreEqual(99, node.ChildNodes[0].Metadata[0]);
            Assert.AreEqual(0, node.Value);
        }

        [Test]
        public void Can_create_nodes_from_first_example()
        {
            var nodeDefinitionString = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            var nodeDefinition = nodeDefinitionString.Split(" ").Select(x => int.Parse(x)).ToArray();
            var node = new Node(nodeDefinition);
            Assert.AreEqual(3, node.Metadata.Count);
            Assert.AreEqual(1, node.Metadata[0]);
            Assert.AreEqual(1, node.Metadata[1]);
            Assert.AreEqual(2, node.Metadata[2]);
            //Main node has 2 child nodes
            Assert.AreEqual(2, node.ChildNodes.Count);
            //Main node's second child has one child
            Assert.AreEqual(99, node.ChildNodes[1].ChildNodes[0].Metadata[0]);
            Assert.AreEqual(138, node.SumAllMetadata);
            Assert.AreEqual(66, node.Value);
        }

        [Test]
        public void Can_get_day_8_part_1_answer()
        {
            var nodeDefinitionString = TestDataProvider.GetTestData();
            var nodeDefinition = nodeDefinitionString.Split(" ").Select(x => int.Parse(x)).ToArray();
            var node = new Node(nodeDefinition);
            Assert.AreEqual(35911, node.SumAllMetadata);
        }

        [Test]
        public void Can_get_day_8_part_2_answer()
        {
            var nodeDefinitionString = TestDataProvider.GetTestData();
            var nodeDefinition = nodeDefinitionString.Split(" ").Select(x => int.Parse(x)).ToArray();
            var node = new Node(nodeDefinition);
            Assert.AreEqual(17206, node.Value);
        }
    }
}
