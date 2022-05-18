using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests
{
    [TestFixture]
    public class RegionTests
    {
        [Test]
        public void Can_create_region()
        {
            var region = new Region("Southwest");
            Assert.IsNotNull(region);
        }

        [Test]
        public void Can_add_store_to_region()
        {
            var region = new Region("Southeast");
            var store = new Store("Store1");
            region.Stores.Add(store);
            Assert.IsNotNull(region.Stores);
            Assert.AreEqual(region.Name, "Southeast");
        }
    }
}