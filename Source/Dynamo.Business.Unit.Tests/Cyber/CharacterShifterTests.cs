using Dynamo.Business.Shared.Cyber;
using NUnit.Framework;
using System.IO;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    [TestFixture]
    public class CharacterShifterTests
    {
        [Test]
        public void Play_with_characters()
        {
            var a = 'a';
            Assert.That((int)a, Is.EqualTo(97));
            var z = 'z';
            Assert.That((int)z, Is.EqualTo(122));
            var A = 'A';
            Assert.That((int)A, Is.EqualTo(65));
            var Z = 'Z';
            Assert.That((int)Z, Is.EqualTo(90));
        }

        [Test]
        public void Shifting_characters_should_work_for_lower_case()
        {
            var a = 'a';
            var b = CharacterShifter.ShiftRight(a, 1);
            Assert.That(b, Is.EqualTo(b));
            var z = 'z';
            var d = CharacterShifter.ShiftRight(z, 4);
            Assert.That(d, Is.EqualTo('d'));
            var e = CharacterShifter.ShiftRight(z, 31);
            Assert.That(e, Is.EqualTo('e'));
        }

        [Test]
        public void Can_shift_negative()
        {
            var b = 'b';
            var a = CharacterShifter.ShiftLeft(b, 1);
            Assert.That(a, Is.EqualTo('a'));


            //var a = 'a';
            var y = CharacterShifter.ShiftLeft(a, 2);
            Assert.That(y, Is.EqualTo('y'));
            var z = 'z';
            var v = CharacterShifter.ShiftLeft(z, 4);
            Assert.That(v, Is.EqualTo('v'));
        }

        [Test]
        public void Shifting_characters_should_work_for_upper_case()
        {
            var a = 'A';
            var b = CharacterShifter.ShiftRight(a, 1);
            Assert.That(b, Is.EqualTo(b));
            var z = 'Z';
            var d = CharacterShifter.ShiftRight(z, 4);
            Assert.That(d, Is.EqualTo('D'));
            var e = CharacterShifter.ShiftRight(z, 31);
            Assert.That(e, Is.EqualTo('E'));
        }

        [Test]
        public void Decipher_may_the_force_be_with_you()
        {
            var unencrypted = "MAY THE FORCE BE WITH YOU";
            var encrypted = CaesarCipher.Encrypt(unencrypted, 4);
            Assert.That(encrypted, Is.EqualTo("QEC XLI JSVGI FI AMXL CSY"));
        }

        [Test]
        public void Get_words_between_bookend_word()
        {
            // Looking for the string between a book end word
            var bookendWord = "triceratops";
            var bookendEncrypted = CaesarCipher.Encrypt(bookendWord, 10);
            Assert.That(bookendEncrypted, Is.EqualTo("dbsmobkdyzc"));
        }

        [Test]
        public void Can_decrypt_week1_part1_file()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("Week1Part1.txt");
            stream.Position = 0;
            string fileContents = string.Empty;
            using (StreamReader reader = new StreamReader(stream))
            {
                fileContents = reader.ReadToEnd();
                Assert.That(fileContents.Length, Is.EqualTo(501447));
            }
            var inbetweenWords = CaesarCipher.GetInbetweenWords(fileContents, 10, "triceratops");
            Assert.That(inbetweenWords, Is.EqualTo("the first four digits of the pin are the current year. the last four digits of the pin are the product of nine and three hundred thirty-seven."));
        }

        [Test]
        public void Can_get_week1_part2()
        {
            var correctShiftValue = 0;
            var magicWord = "triceratops";

            string fileContents = string.Empty;
            for (var i = 0; i < 999; i++)
            {
                var stream = FileGetter.GetMemoryStreamFromFile("Week1Part2.txt");
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    fileContents = reader.ReadToEnd();
                }
                var decryptedFileContents = CaesarCipher.Decrypt(fileContents, i);
                if (decryptedFileContents.Contains(magicWord))
                {
                    correctShiftValue = i;
                    break;
                }
            }
            var inbetweenWords = CaesarCipher.GetInbetweenWords(fileContents, correctShiftValue, "triceratops");
            Assert.That(inbetweenWords, Is.EqualTo("the first four digits of the pin are the product of seventeen and one hundred twenty-five. the next two digits are the ninth prime number. the final two digits are the tenth prime number."));
        }

    }
}
