using Dynamo.Business.Shared.Cyber.Scanner;
using Dynamo.Business.Shared.Cyber.Stenography;
using NUnit.Framework;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.Drawing;
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
            Assert.That(text, Is.EqualTo("4817263942689053"));
        }

        [Test]
        public void Can_convert_least_significant_bits_to_ascii()
        {
            var onesAndZeros = "0110100001101001";
            var hi = MessageAnalyzer.GetStringFromOnesAndZeroString(onesAndZeros, 0);
            Assert.That(hi, Is.EqualTo("hi"));
        }

        [Test]
        public void Can_convert_least_significant_bits_to_ascii_2()
        {
            var onesAndZeros = "1000110101001100001011011010110001001101111011011101001110111001001110110100000010001010011100100100101111110001";
            var hi = MessageAnalyzer.GetStringFromOnesAndZeroString(onesAndZeros, 0);
            var winningOffset = 0;
            var winningMessage = string.Empty;
            for (int offset = 0; offset < 8; offset++)
            {
                var message = MessageAnalyzer.GetStringFromOnesAndZeroString(onesAndZeros, offset).ToLower();
                if (message.Contains("jambon"))
                {
                    winningOffset = offset;
                    winningMessage = message;
                }
            }
            Assert.That(winningOffset, Is.EqualTo(3));
        }

        [Test]
        public void Can_solve_week_5_spring_2025_challenge()
        {
            var wholeFile = ExtractLsbs_SystemDrawing("C:\\users\\user1\\Downloads\\week5_security_cam.png");
            var onesAndZeros = MessageAnalyzer.GetBytesFromLeastSignificantDights(wholeFile.ToArray());
            Assert.That(onesAndZeros.Length, Is.GreaterThan(0));

            var messageCount = 0;
            var winningOffset = 0;
            var winningMessage = string.Empty;
            // Narrowed it down to offset = 1. I'm leaving the loop here to keep track of how to handle the offset.
            for (int offset = 1; offset < 2; offset++)
            {
                var message = MessageAnalyzer.GetStringFromOnesAndZeroString(onesAndZeros, offset);
                if (message.Contains("0xdeadbeef"))
                {
                    messageCount++;
                    winningOffset = offset;
                    winningMessage = message;
                }
            }
            Assert.That(messageCount, Is.EqualTo(1));
            Assert.That(winningOffset, Is.EqualTo(1));
            var secretMessage = FolderScanner.FindMessage(winningMessage, "0xdeadbeef");
            Assert.That(secretMessage, Is.EqualTo("R40wS0nmCYGtdZ53MPMCHg8CGuFY117U"));
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

        public static List<byte> ExtractLsbs_SystemDrawing(string path)
        {
            using Bitmap bmp = (Bitmap)Image.FromFile(path);
            var bits = new List<byte>(bmp.Width * bmp.Height);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);       // grayscale → R == G == B
                    bits.Add((byte)(c.R & 1));          // take LSB of the byte
                }
            }

            return bits;
        }

        private static byte[] GetOneBigByteArray(string fileName)
        {
            using var stream = FileGetter.GetMemoryStreamFromFile(fileName);

            // If the helper already gave us a MemoryStream we can return its buffer directly.
            if (stream is MemoryStream ms)
                return ms.ToArray();

            // Otherwise copy the raw bytes into our own MemoryStream.
            using var output = new MemoryStream();
            stream.CopyTo(output);       // copies in 4 KB blocks by default
            return output.ToArray();
        }
    }
}
