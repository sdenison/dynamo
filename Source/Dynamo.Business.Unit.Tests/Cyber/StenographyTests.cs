using Dynamo.Business.Shared.Cyber.Stenography;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class StenographyTests
    {
        [Test]
        public void Can_get_file_contents()
        {
            var fileContents = GetOneBigString("week5_part1_message.txt");
            Assert.That(fileContents.StartsWith("security update: "));
        }

        [Test]
        public void Can_parse_spaces()
        {
            var fileContents = GetOneBigString("week5_part1_message.txt");
            var messageAnalyzer = new MessageAnalyzer();
            var spaces = messageAnalyzer.GetSpacesAfterPunctuation(fileContents);
            // The spaces are supposed to represent bytes so it should be divisible by 8
            Assert.That(spaces.Count % 8, Is.EqualTo(0));

        }


        private static string GetOneBigString(string fileName)
        {
            var stream = FileGetter.GetMemoryStreamFromFile(fileName);
            var sb = new StringBuilder();

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    sb.Append(line.Trim());
            }
            return sb.ToString();
        }
    }
}
