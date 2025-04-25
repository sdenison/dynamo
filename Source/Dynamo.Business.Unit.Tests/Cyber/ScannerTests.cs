using Dynamo.Business.Shared.Cyber.Scanner;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class ScannerTests
    {
        [Test]
        public void Can_find_text_between_start_and_end()
        {
            var prefix = "alksdjfjfeklwjfklefj;alwkefjklaejf";
            var start = "4920414d2054574f20464f4f4c53";
            var middle = "aabbbbbweffkefjaaa"; // Has 5 a's
            var end = "444945204e4f542c20504f4f52204445415448";
            var suffix = "alksdjfjfeklwjfklefj;alwkefjklaejf";
            var wholeText = prefix + start + middle + end + suffix;
            var message = FolderScanner.FindMessage(wholeText, start, end);
            Assert.That(message, Is.EqualTo(middle));
            Assert.That(FolderScanner.CountTheChar(message, 'a'), Is.EqualTo(5));
        }

        [Test]
        public void Can_count_number_of_char_a_in_files()
        {
            var folder = "D:\\temp\\Problem2SystemFiles";
            var start = "4920414d2054574f20464f4f4c53";
            var end = "444945204e4f542c20504f4f52204445415448";
            var message = FolderScanner.ScanFolder(folder, start, end);
            Assert.That(FolderScanner.CountTheChar(message, 'a'), Is.EqualTo(820));
        }
    }
}
