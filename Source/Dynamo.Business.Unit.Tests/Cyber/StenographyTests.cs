using Dynamo.Business.Shared.Cyber.Scanner;
using Dynamo.Business.Shared.Cyber.Stenography;
using NUnit.Framework;
using System.Collections.Generic;
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
            Assert.That(spaces.Count % 8, Is.EqualTo(0));
        }

        [Test]
        public void Can_get_bytes_from_list_of_spaces()
        {
            var fileContents = GetOneBigString("week5_part1_message.txt");
            var messageAnalyzer = new MessageAnalyzer();
            var spaces = messageAnalyzer.GetSpacesAfterPunctuation(fileContents);
            var bytes = messageAnalyzer.GetBytesFromSpaces(spaces);
            var realBytes = new List<byte>();
            foreach (var b in bytes)
            {
                realBytes.Add(MessageAnalyzer.ParseByte(b));
            }
            var text = Encoding.UTF8.GetString(realBytes.ToArray());
            Assert.That(text, Is.EqualTo("37482910"));
        }

        [Test]
        public void Can_get_part_2_week_5_spring_2025()
        {
            var wholeText = GetOneBigString("week5_part2_document.txt");
            var delimiter = "0xdeadbeef";
            var message = FolderScanner.FindMessage(wholeText, delimiter);
            var bytes = MessageAnalyzer.GetBytesFromCase(message);
            var realBytes = new List<byte>();
            foreach (var b in bytes)
            {
                realBytes.Add(MessageAnalyzer.ParseByte(b));
            }
            var text = Encoding.UTF8.GetString(realBytes.ToArray());
            Assert.That(text, Is.EqualTo("37482910"));
        }


        private static string GetOneBigString(string fileName)
        {
            var stream = FileGetter.GetMemoryStreamFromFile(fileName);
            var sb = new StringBuilder();

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    sb.Append(line);
            }
            return sb.ToString();
        }
    }
}
