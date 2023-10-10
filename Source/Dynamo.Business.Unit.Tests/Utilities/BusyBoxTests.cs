using Dynamo.Business.Shared.Utilities;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Utilities
{
    [TestFixture]
    public class BusyBoxTests
    {
        [Test]
        public void Can_get_BusyBox_stream()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("BusyBox1Second.txt");
            int secondsToSleep = BusyBox.GetSecondFromStream(stream);
            Assert.AreEqual(1, secondsToSleep);
        }
    }
}
