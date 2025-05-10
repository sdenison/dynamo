using Dynamo.Business.Shared.Cyber.Passwords;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        [Test]
        public void Can_get_hash_values_for_examples()
        {
            var password = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt").First();
            var updatedPassword = GetPasswordsFromFile("tmyc-week4-pt1-example2.txt").First();

            var passwordSha1 = PasswordAnalyzer.ComputeSha1Hash(password);
            Assert.That(passwordSha1, Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));
            var updatedPasswordSha1 = PasswordAnalyzer.ComputeSha1Hash(updatedPassword);
            Assert.That(updatedPasswordSha1, Is.EqualTo("6a531ecd01ed447c395a7431dcb65ec82d22eb66"));

            var differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordSha1, updatedPasswordSha1);
            Assert.That(differencePercentage, Is.EqualTo(87.5));
        }

        [Test]
        public void Can_get_hash_values_for_example_lists()
        {
            var passwords = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt");
            var updatedPasswords = GetPasswordsFromFile("tmyc-week4-pt1-example2.txt");

            var passwordsSha1 = PasswordAnalyzer.ComputeSha1Hash(passwords);
            var updatedPasswordsSha1 = PasswordAnalyzer.ComputeSha1Hash(updatedPasswords);

            var differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha1, updatedPasswordsSha1);
            Assert.That(differencePercentage, Is.EqualTo(87.5));
        }

        [Test]
        public void Can_get_sha1_list_of_passwords()
        {
            var passwords = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt");
            var hashedPasswords = PasswordAnalyzer.ComputeSha1Hash(passwords);
            Assert.That(hashedPasswords[0], Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));
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
