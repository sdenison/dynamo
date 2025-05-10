using Dynamo.Business.Shared.Cyber.Passwords;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class PasswordAnalyzerTests
    {
        [TestCase("!TMYC2025!", 30)]
        [TestCase("K9$pL!xQz&23", 43.02)]
        public void Can_get_entropy_numbers_for_example_2(string password, double expectedEntropy)
        {
            var example2Entropy = PasswordAnalyzer.GetPasswordEntropy(password);
            var tolerance = 0.001;
            Assert.That(example2Entropy, Is.EqualTo(expectedEntropy).Within(tolerance));
        }

        [Test]
        public void Can_solve_TMYC_2025_week_4_part_1()
        {
            var passwords = GetPasswordsFromFile("week4_pt1_input.txt");
            Assert.That(passwords.Count, Is.EqualTo(200));
            var entropyThreshold = 65;
            var numberOfGoodPasswords = 0;
            foreach (var password in passwords)
            {
                if (PasswordAnalyzer.GetPasswordEntropy(password) >= entropyThreshold)
                    numberOfGoodPasswords++;
            }
            Assert.That(numberOfGoodPasswords, Is.EqualTo(81));
        }

        public List<string> GetPasswordsFromFile(string fileName)
        {
            var passwordFile = FileGetter.GetMemoryStreamFromFile(fileName);
            var passwords = new List<string>();

            using (var reader = new StreamReader(passwordFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    passwords.Add(line);
                }
            }
            return passwords;
        }
    }
}
