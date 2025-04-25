using Dynamo.Business.Shared.Cyber.Scanner;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class ScannerTests
    {
        [Test]
        public void Can_find_text_between_start_and_end()
        {
            var folder = "D:\\temp\\Problem2SystemFiles";
            var prefix = "alksdjfjfeklwjfklefj;alwkefjklaejf";
            var start = "4920414d2054574f20464f4f4c53";
            var middle = "aabbbbbweffkefjaaa"; // Has 5 a's
            var end = "444945204e4f542c20504f4f52204445415448";
            var suffix = "alksdjfjfeklwjfklefj;alwkefjklaejf";
            var wholeText = prefix + start + middle + end + suffix;
            var message = FolderScanner.FindMessage(wholeText, start, end);
            Assert.That(message, Is.EqualTo(middle));
        }
    }
}
