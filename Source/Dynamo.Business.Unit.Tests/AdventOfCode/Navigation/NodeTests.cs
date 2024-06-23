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
            Assert.That(1, Is.EqualTo(node.Metadata.Count));
            Assert.That(99, Is.EqualTo(node.Metadata[0]));
            Assert.That(99, Is.EqualTo(node.Value));
        }

        [Test]
        public void Can_create_a_node_with_one_child()
        {
            var nodeDefinition = new[] { 1, 1, 0, 1, 99, 2 };
            var node = new Node(nodeDefinition);
            Assert.That(2, Is.EqualTo(node.Metadata[0]));
            Assert.That(99, Is.EqualTo(node.ChildNodes[0].Metadata[0]));
            Assert.That(0, Is.EqualTo(node.Value));
        }

        [Test]
        public void Can_create_nodes_from_first_example()
        {
            var nodeDefinitionString = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            var node = new Node(nodeDefinitionString);
            Assert.That(3, Is.EqualTo(node.Metadata.Count));
            Assert.That(1, Is.EqualTo(node.Metadata[0]));
            Assert.That(1, Is.EqualTo(node.Metadata[1]));
            Assert.That(2, Is.EqualTo(node.Metadata[2]));
            //Main node has 2 child nodes
            Assert.That(2, Is.EqualTo(node.ChildNodes.Count));
            //Main node's second child has one child
            Assert.That(99, Is.EqualTo(node.ChildNodes[1].ChildNodes[0].Metadata[0]));
            Assert.That(138, Is.EqualTo(node.SumAllMetadata));
            Assert.That(66, Is.EqualTo(node.Value));
        }

        [Test]
        public void Can_get_day_8_part_1_answer()
        {
            var nodeDefinitionString = TestDataProvider.GetTestData();
            var node = new Node(nodeDefinitionString);
            Assert.That(35911, Is.EqualTo(node.SumAllMetadata));
        }

        [Test]
        public void Can_get_day_8_part_2_answer()
        {
            var nodeDefinitionString = TestDataProvider.GetTestData();
            var node = new Node(nodeDefinitionString);
            Assert.That(17206, Is.EqualTo(node.Value));
        }
    }
}
