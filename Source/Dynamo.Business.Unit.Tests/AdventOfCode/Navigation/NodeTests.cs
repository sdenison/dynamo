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
        }

        [Test]
        public void Can_create_a_node_with_one_child()
        {
            var nodeDefinition = new[] { 1, 1, 0, 1, 99, 2 };
            var node = new Node(nodeDefinition);
            Assert.AreEqual(2, node.Metadata[0]);
            Assert.AreEqual(99, node.ChildNodes[0].Metadata[0]);
        }
    }
}
