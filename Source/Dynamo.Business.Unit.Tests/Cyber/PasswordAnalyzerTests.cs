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
        /* -------------------------------------------------- *
         *  Entropy                                           *
         * -------------------------------------------------- */

        [TestCase("!TMYC2025!", 30)]
        [TestCase("K9$pL!xQz&23", 43.02)]
        public void Can_get_entropy_numbers_for_example_2(string password, double expectedEntropy)
        {
            var entropy = PasswordAnalyzer.GetPasswordEntropy(password);
            Assert.That(entropy, Is.EqualTo(expectedEntropy).Within(0.001));
        }

        [Test]
        public void Can_solve_TMYC_2025_week_4_part_1()
        {
            var passwords = GetPasswordsFromFile("week4_pt1_input.txt");
            Assert.That(passwords.Count, Is.EqualTo(200));

            const double entropyThreshold = 65;
            var good = passwords.Count(p => PasswordAnalyzer.GetPasswordEntropy(p) >= entropyThreshold);

            Assert.That(good, Is.EqualTo(81));
        }

        /* -------------------------------------------------- *
         *  Single-string hashing                             *
         * -------------------------------------------------- */

        [Test]
        public void Can_get_hash_values_for_examples()
        {
            var password = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt").First();
            var updatedPassword = GetPasswordsFromFile("tmyc-week4-pt1-example2.txt").First();

            var passwordSha1 = PasswordAnalyzer.Sha1(password);
            var updatedPasswordSha1 = PasswordAnalyzer.Sha1(updatedPassword);

            Assert.That(passwordSha1, Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));
            Assert.That(updatedPasswordSha1, Is.EqualTo("6a531ecd01ed447c395a7431dcb65ec82d22eb66"));

            var difference = PasswordAnalyzer.DifferencePercentage(passwordSha1, updatedPasswordSha1);
            Assert.That(difference, Is.EqualTo(87.5));
        }

        /* -------------------------------------------------- *
         *  List hashing                                      *
         * -------------------------------------------------- */

        [Test]
        public void Can_get_hash_values_for_example_lists()
        {
            var passwords = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt");
            var updatedPasswords = GetPasswordsFromFile("tmyc-week4-pt1-example2.txt");

            var passwordsSha1 = PasswordAnalyzer.Sha1(passwords);
            var updatedPasswordsSha1 = PasswordAnalyzer.Sha1(updatedPasswords);

            var difference = PasswordAnalyzer.DifferencePercentage(passwordsSha1, updatedPasswordsSha1);
            Assert.That(difference, Is.EqualTo(87.5));
        }

        [Test]
        public void Can_get_sha1_list_of_passwords()
        {
            var passwords = GetPasswordsFromFile("tmyc-week4-pt1-example1.txt");
            var hashedPasswords = PasswordAnalyzer.Sha1(passwords);

            Assert.That(hashedPasswords[0], Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));
        }

        /* -------------------------------------------------- *
         *  Multiple algorithms                               *
         * -------------------------------------------------- */

        [Test]
        public void Can_get_different_hashes()
        {
            const string password = "!TMYC2025!";

            Assert.That(PasswordAnalyzer.Sha1(password),
                Is.EqualTo("edd7f5c3cbbdac6a1d3aae3b0dbae0e975f56413"));

            Assert.That(PasswordAnalyzer.Sha256(password),
                Is.EqualTo("6a8982572c7a1caa4a89232836a3ebf33785cf9da9079756ce842d71c7bf1e54"));

            Assert.That(PasswordAnalyzer.Sha512(password),
                Is.EqualTo("f36304521003ab238c5472a89a81c01d44ae13ed8196bf3185a3069724ce219375e80e9d2a3daa01fe2c12207d9315b462effee16dc6a9c9a41f58057bccb7c0"));

            Assert.That(PasswordAnalyzer.Blake2b(password),
                Is.EqualTo("203b8e5a01eb375ced38fed7cbbc1fdbc4ab346c6562396c4324317f293571f481a0de56130159b186e7f8793b9d3d2388064082ab62e8e7070c797b610d5c51"));

            Assert.That(PasswordAnalyzer.Md5(password),
                Is.EqualTo("0734950cc8ebafcc0c7c28a2ebae188c"));
        }

        /* -------------------------------------------------- *
         *  Week-4 part-2 solutions                           *
         * -------------------------------------------------- */

        [Test]
        public void Can_solve_TMYC_spring_2025_week_4_part_2()
        {
            var passwords = GetPasswordsFromFile("week4_pt1_input.txt");
            var updatedPasswords = GetPasswordsFromFile("week4_pt2_input.txt");

            var diffs = new Dictionary<string, double>
            {
                ["SHA1"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha1(passwords),
                                 PasswordAnalyzer.Sha1(updatedPasswords)),

                ["SHA256"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha256(passwords),
                                 PasswordAnalyzer.Sha256(updatedPasswords)),

                ["SHA512"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha512(passwords),
                                 PasswordAnalyzer.Sha512(updatedPasswords)),

                ["BLAKE2B"] = PasswordAnalyzer.DifferencePercentage(
                                  PasswordAnalyzer.Blake2b(passwords),
                                  PasswordAnalyzer.Blake2b(updatedPasswords)),

                ["MD5"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Md5(passwords),
                                 PasswordAnalyzer.Md5(updatedPasswords))
            };

            Assert.That(diffs.Count, Is.EqualTo(5));
        }

        [Test]
        public void Can_solve_TMYC_spring_2025_week_4_part_2_big_string()
        {
            var passwords = GetOneBigString("week4_pt1_input.txt");
            var updatedPasswords = GetOneBigString("week4_pt2_input.txt");

            var diffs = new Dictionary<string, double>
            {
                ["SHA1"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha1(passwords),
                                 PasswordAnalyzer.Sha1(updatedPasswords)),

                ["SHA256"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha256(passwords),
                                 PasswordAnalyzer.Sha256(updatedPasswords)),

                ["SHA512"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Sha512(passwords),
                                 PasswordAnalyzer.Sha512(updatedPasswords)),

                ["BLAKE2B"] = PasswordAnalyzer.DifferencePercentage(
                                  PasswordAnalyzer.Blake2b(passwords),
                                  PasswordAnalyzer.Blake2b(updatedPasswords)),

                ["MD5"] = PasswordAnalyzer.DifferencePercentage(
                                 PasswordAnalyzer.Md5(passwords),
                                 PasswordAnalyzer.Md5(updatedPasswords))
            };

            Assert.That(diffs.Count, Is.EqualTo(5));
        }

        /* -------------------------------------------------- *
         *  Helpers                                           *
         * -------------------------------------------------- */
        private static List<string> GetPasswordsFromFile(string fileName)
        {
            var stream = FileGetter.GetMemoryStreamFromFile(fileName);
            var list = new List<string>();

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line.Trim());
            }
            return list;
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

