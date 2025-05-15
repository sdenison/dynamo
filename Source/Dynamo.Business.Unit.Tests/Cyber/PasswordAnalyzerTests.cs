using Dynamo.Business.Shared.Cyber.Passwords;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        [Test]
        public void Can_get_different_hashes()
        {
            var password = "!TMYC2025!";

            var sha1 = PasswordAnalyzer.ComputeSha1Hash(password);
            Assert.That(sha1, Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));

            var sha256 = PasswordAnalyzer.ComputeSha256Hash(password);
            Assert.That(sha256, Is.EqualTo("6a8982572c7a1caa4a89232836a3ebf33785cf9da9079756ce842d71c7bf1e54"));

            var sha512 = PasswordAnalyzer.ComputeSha512Hash(password);
            Assert.That(sha512, Is.EqualTo("f36304521003ab238c5472a89a81c01d44ae13ed8196bf3185a3069724ce219375e80e9d2a3daa01fe2c12207d9315b462effee16dc6a9c9a41f58057bccb7c0"));

            var blake2b = PasswordAnalyzer.ComputeBlake2bHash(password);
            Assert.That(blake2b, Is.EqualTo("203b8e5a01eb375ced38fed7cbbc1fdbc4ab346c6562396c4324317f293571f481a0de56130159b186e7f8793b9d3d2388064082ab62e8e7070c797b610d5c51"));

            var md5 = PasswordAnalyzer.ComputeMd5Hash(password);
            Assert.That(md5, Is.EqualTo("0734950cc8ebafcc0c7c28a2ebae188c"));
        }

        [Test]
        public void Can_solve_TMYC_spring_2025_week_4_part_2()
        {
            var passwords = GetPasswordsFromFile("week4_pt1_input.txt");
            var updatedPasswords = GetPasswordsFromFile("week4_pt2_input.txt");
            var diffPercentageDictionary = new Dictionary<string, double>();

            var passwordsSha1 = PasswordAnalyzer.ComputeSha1Hash(passwords);
            var updatedPasswordsSha1 = PasswordAnalyzer.ComputeSha1Hash(updatedPasswords);
            var differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha1, updatedPasswordsSha1);
            diffPercentageDictionary.Add("SHA1", differencePercentage);

            var passwordsSha256 = PasswordAnalyzer.ComputeSha256Hash(passwords);
            var updatedPasswordsSha256 = PasswordAnalyzer.ComputeSha256Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha256, updatedPasswordsSha256);
            diffPercentageDictionary.Add("SHA256", differencePercentage);

            var passwordsSha512 = PasswordAnalyzer.ComputeSha512Hash(passwords);
            var updatedPasswordsSha512 = PasswordAnalyzer.ComputeSha512Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha512, updatedPasswordsSha512);
            diffPercentageDictionary.Add("SHA512", differencePercentage);

            var passwordsBlake = PasswordAnalyzer.ComputeBlake2bHash(passwords);
            var updatedPasswordsBlake = PasswordAnalyzer.ComputeBlake2bHash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsBlake, updatedPasswordsBlake);
            diffPercentageDictionary.Add("BLAKE2B", differencePercentage);

            var passwordsMd5 = PasswordAnalyzer.ComputeMd5Hash(passwords);
            var updatedPasswordsMd5 = PasswordAnalyzer.ComputeMd5Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsMd5, updatedPasswordsMd5);
            diffPercentageDictionary.Add("MD5", differencePercentage);

            Assert.That(diffPercentageDictionary.Count, Is.EqualTo(5));
        }

        [Test]
        public void Can_solve_TMYC_spring_2025_week_4_part_2_2()
        {
            var passwords = GetOneBigString("week4_pt1_input.txt");
            var updatedPasswords = GetOneBigString("week4_pt2_input.txt");
            var diffPercentageDictionary = new Dictionary<string, double>();

            var passwordsSha1 = PasswordAnalyzer.ComputeSha1Hash(passwords);
            var updatedPasswordsSha1 = PasswordAnalyzer.ComputeSha1Hash(updatedPasswords);
            var differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha1, updatedPasswordsSha1);
            diffPercentageDictionary.Add("SHA1", differencePercentage);

            var passwordsSha256 = PasswordAnalyzer.ComputeSha256Hash(passwords);
            var updatedPasswordsSha256 = PasswordAnalyzer.ComputeSha256Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha256, updatedPasswordsSha256);
            diffPercentageDictionary.Add("SHA256", differencePercentage);

            var passwordsSha512 = PasswordAnalyzer.ComputeSha512Hash(passwords);
            var updatedPasswordsSha512 = PasswordAnalyzer.ComputeSha512Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsSha512, updatedPasswordsSha512);
            diffPercentageDictionary.Add("SHA512", differencePercentage);

            var passwordsBlake = PasswordAnalyzer.ComputeBlake2bHash(passwords);
            var updatedPasswordsBlake = PasswordAnalyzer.ComputeBlake2bHash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsBlake, updatedPasswordsBlake);
            diffPercentageDictionary.Add("BLAKE2B", differencePercentage);

            var passwordsMd5 = PasswordAnalyzer.ComputeMd5Hash(passwords);
            var updatedPasswordsMd5 = PasswordAnalyzer.ComputeMd5Hash(updatedPasswords);
            differencePercentage = PasswordAnalyzer.GetDifferencePercentage(passwordsMd5, updatedPasswordsMd5);
            diffPercentageDictionary.Add("MD5", differencePercentage);

            Assert.That(diffPercentageDictionary.Count, Is.EqualTo(5));
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
                    var utf8Encoded = System.Text.Encoding.UTF8.GetBytes(line.Trim().TrimEnd('\r', '\n', ' ', '\t').ToString());
                    passwords.Add(line.Trim().TrimEnd('\r', '\n', ' ', '\t'));
                    //passwords.Add(System.Text.Encoding.UTF8.GetBytes(line.Trim().TrimEnd('\r', '\n', ' ', '\t')).ToString());

                }
            }
            return passwords;
        }


        public string GetOneBigString(string fileName)
        {
            var passwordFile = FileGetter.GetMemoryStreamFromFile(fileName);
            var passwords = new StringBuilder();

            using (var reader = new StreamReader(passwordFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //var utf8Encoded = System.Text.Encoding.UTF8.GetBytes(line.Trim().TrimEnd('\r', '\n', ' ', '\t').ToString());
                    passwords.Append(line.Trim().TrimEnd('\r', '\n', ' ', '\t'));
                    //passwords.Add(line.Trim().TrimEnd('\r', '\n', ' ', '\t'));
                    //passwords.Add(System.Text.Encoding.UTF8.GetBytes(line.Trim().TrimEnd('\r', '\n', ' ', '\t')).ToString());

                }
            }
            return passwords.ToString();
        }


        //public static List<string> GetPasswordsFromFile(string fileName)
        //{
        //    var passwords = new List<string>();

        //    using var stream = FileGetter.GetMemoryStreamFromFile(fileName);

        //    // The last ‘true’ makes the reader throw if it sees an invalid UTF‑8 sequence.
        //    using var reader = new StreamReader(
        //        stream,
        //        new UTF8Encoding(encoderShouldEmitUTF8Identifier: false,
        //                         throwOnInvalidBytes: true),
        //        detectEncodingFromByteOrderMarks: false,
        //        bufferSize: 1024,
        //        leaveOpen: false);

        //    string? line;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.Trim('\r', '\n', ' ', '\t');
        //        if (line.Length == 0) continue;          // skip blank lines if desired
        //        passwords.Add(line);
        //    }

        //    return passwords;
        //}

    }
}
