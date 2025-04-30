using Dynamo.Business.Shared.Cyber.Scanner;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
        public void Can_get_spring_2025_week_2_part_3()
        {
            var folder = "D:\\temp\\Problem2SystemFiles";
            var start = "4920414d2054574f20464f4f4c53";
            var end = "444945204e4f542c20504f4f52204445415448";
            var message = FolderScanner.ScanFolder(folder, start, end);

            var key = Encoding.ASCII.GetBytes("63382816");
            var messageAsBytes = Convert.FromHexString(message);
            var decrypted = new StringBuilder();
            for (var i = 0; i < messageAsBytes.Length; i++)
            {
                decrypted.Append((char)(messageAsBytes[i] ^ key[i % key.Length]));
            }
            Assert.That(decrypted.ToString(), Does.StartWith("Meet me at the flagpole, 4:30pm."));
        }

        [Test]
        public void Can_get_challenge_problem_spring_2025_week_2()
        {
            var folder = "D:\\temp\\Problem2SystemFiles";
            var start = "4920414d2054574f20464f4f4c53";
            var end = "444945204e4f542c20504f4f52204445415448";
            var message = FolderScanner.ScanFolder(folder, start, end);

            var key = Encoding.ASCII.GetBytes("63382816");
            var messageAsBytes = Convert.FromHexString(message);
            var decrypted = new StringBuilder();
            for (var i = 0; i < messageAsBytes.Length; i++)
            {
                decrypted.Append((char)(messageAsBytes[i] ^ key[i % key.Length]));
            }
            var altered = decrypted.ToString();
            Assert.That(decrypted.ToString(), Does.StartWith("Meet me at the flagpole, 4:30pm."));

            altered = altered.Replace("4:30pm", "10:30am");

            // This cuts the first sentenace Chamberlain says
            //altered = Regex.Replace(
            //    altered,
            //    @"CHAMBERLAIN\..*?[.?!]\s?",  // Regex pattern
            //    "",                        // Replace with nothing
            //    RegexOptions.Singleline    // Allow '.' to match across lines if needed
            //);

            // This cuts all text said by Chanberlain
            altered = Regex.Replace(
                altered,
                @"CHAMBERLAIN\..*?(?=[A-Z]+\.)",
                "",
                RegexOptions.Singleline
            );


            //altered = altered.Replace(" CHAMBERLAIN. ", " ");
            //altered = altered.Replace("CHAMBERLAIN. ", " ");
            altered = altered.Replace(" Chamberlain  and Lord Sandys", "");
            altered = altered.Replace(" Chamberlain, Lord Sandys and Sir Thomas Lovell", "");
            //altered = altered.Replace(".  ", ". ");
            //altered = altered.Replace("  ", " ");
            altered = altered.Replace(" CHAMBERLAIN.", "");
            altered = altered.Replace("Look out there, some of ye.", "");
            altered = altered.Trim();
            altered = altered + " ";
            var xxx = altered;

            //var key = Encoding.ASCII.GetBytes("63382816");
            //var messageAsBytes = Convert.FromHexString(message);
            //var decrypted = new StringBuilder();



            //byte[] encrypted = new byte[altered.Length];
            //for (var i = 0; i < altered.Length; i++)
            //{
            //    encrypted[i] = (byte)(altered[i] ^ key[i % key.Length]);
            //}


            // Convert the altered string to a UTF-8 byte array
            byte[] alteredBytes = Encoding.UTF8.GetBytes(altered);
            byte[] encrypted = new byte[alteredBytes.Length];

            for (int i = 0; i < alteredBytes.Length; i++)
            {
                encrypted[i] = (byte)(alteredBytes[i] ^ key[i % key.Length]);
            }

            //var hexString = BitConverter.ToString(encrypted).Replace("-", "").ToLower();
            var hexString = Convert.ToHexString(encrypted).ToLower();

            string encodedFilePath = @"D:\temp\flagpole\encoded.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(encodedFilePath)); // Make sure the folder exists
            File.WriteAllText(encodedFilePath, altered);

            string encodedHexPath = @"D:\temp\flagpole\encoded-hex.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(encodedHexPath)); // Make sure the folder exists
            File.WriteAllText(encodedHexPath, hexString);

            var x = hexString;
            var numAInPayload = FolderScanner.CountTheChar(altered, 'a');// 703 is too high
            Assert.That(FolderScanner.CountTheChar(hexString, 'a'), Is.LessThan(75));  // 756 too high
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
