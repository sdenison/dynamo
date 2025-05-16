using Dynamo.Business.Shared.Cyber.Scanner;
using Dynamo.Business.Shared.Cyber.Stenography;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Assert.That(text, Is.EqualTo("4817263942689053"));
        }

        [Test]
        public void Can_solve_week_5_spring_2025_challenge()
        {
            var wholeFile = GetOneBigByteArray("week5_security_cam.png");
            var onesAndZeros = MessageAnalyzer.GetBytesFromLeastSignificantDights(wholeFile);
            Assert.That(onesAndZeros.Count, Is.GreaterThan(0));

            for (int offset = 0; offset < 8; offset++)
            {
                var message = MessageAnalyzer.GetStringFromOnesAndZeroString(onesAndZeros, offset).ToLower();
                if (message.Contains("deadbeef"))
                {
                    var xxxxx = offset;
                }
            }
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

        private static byte[] GetOneBigByteArray(string fileName)
        {
            // Get the file as a stream (your existing helper)
            using var stream = FileGetter.GetMemoryStreamFromFile(fileName);

            // We’ll build the result in a MemoryStream
            using var output = new MemoryStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // Convert each line to UTF-8 bytes and write them out
                var lineBytes = Encoding.UTF8.GetBytes(line);
                output.Write(lineBytes, 0, lineBytes.Length);
            }

            return output.ToArray();   // one contiguous byte[] without newline bytes
        }

    }
}
