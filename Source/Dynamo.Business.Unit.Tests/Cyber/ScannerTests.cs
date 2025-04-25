using Dynamo.Business.Shared.Cyber.Scanner;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

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

        [Test]
        public void Can_parse_string_to_byte()
        {
            var str = "0000001";
            var b = KeyfileReader.ParseByte(str);
            Assert.That(b, Is.EqualTo(1));
            str = "0000010";
            b = KeyfileReader.ParseByte(str);
            Assert.That(b, Is.EqualTo(2));
            Assert.That(KeyfileReader.AddInvertedParityBit(b), Is.EqualTo(5));
        }

        [Test]
        public void Can_invert_binary_digits()
        {
            var str = "00000010";
            var b = KeyfileReader.ParseByte(str);
            var inverted = KeyfileReader.InvertDigits(b);
            var invertedStr = "11111101";
            Assert.That(inverted, Is.EqualTo(253));
        }

        [Test]
        public void Can_solve_spring_2025_week_2_part_2()
        {
            var keyFileStream = FileGetter.GetMemoryStreamFromFile("Problem2Keyfile.txt");
            var total = 0;
            using (StreamReader inputReader = new StreamReader(keyFileStream))
            {
                var keys = KeyfileReader.ConvertKeys(inputReader.ReadToEnd());
                foreach (var key in keys)
                {
                    total += key;
                }
            }
            Assert.That(total, Is.EqualTo(63382816));
        }

        [Test]
        public void Can_xor_utf8_string()
        {
            var key = new byte[] { 0x42, 0x19 };
            string encrypted = KeyfileReader.XorUtf8String("hello world", key);
            string decrypted = KeyfileReader.XorUtf8String(encrypted, key);  // XOR again to decrypt

            Console.WriteLine(decrypted);
        }

        [Test]
        public void Can_get_spring_2025_week_2_part_3()
        {
            var folder = "D:\\temp\\Problem2SystemFiles";
            var start = "4920414d2054574f20464f4f4c53";
            var end = "444945204e4f542c20504f4f52204445415448";
            var message = FolderScanner.ScanFolder(folder, start, end);
            var keys = GetKeys();
            var keyIndex = 0;

            //var decrypted = new StringBuilder();
            byte[] decrypted = new byte[message.Length];
            foreach (var charToXor in Encoding.UTF8.GetBytes(message))
            {
                var decryptedChar = charToXor ^ keys[keyIndex % keys.Length];
                decrypted[keyIndex] = (byte)decryptedChar;
                keyIndex++;
            }

            string decrypted2 = KeyfileReader.XorUtf8String(message, keys);  // XOR again to decrypt

            Assert.That(decrypted2, Is.EqualTo("xxx"));
        }

        public static byte[] GetKeys()
        {
            var keyFileStream = FileGetter.GetMemoryStreamFromFile("Problem2Keyfile.txt");
            using (StreamReader inputReader = new StreamReader(keyFileStream))
            {
                return KeyfileReader.ConvertKeys(inputReader.ReadToEnd());
            }
        }
    }
}
