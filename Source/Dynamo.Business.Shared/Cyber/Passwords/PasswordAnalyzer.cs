using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dynamo.Business.Shared.Cyber.Passwords
{
    public static class PasswordAnalyzer
    {
        // ---------- Entropy -------------------------------------------------
        private static double Log2(double x) => Math.Log(x, 2);

        public static double GetPasswordEntropy(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            int unique = password.Distinct().Count();
            return password.Length * Log2(unique);
        }

        // ---------- Hash helpers -------------------------------------------
        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private static string HashToHex(HashAlgorithm alg, string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] hash = alg.ComputeHash(data);
            return BytesToHex(hash);
        }

        public static IReadOnlyList<string> ComputeHashes(
            IEnumerable<string> inputs,
            Func<HashAlgorithm> algoFactory)
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs));
            if (algoFactory == null) throw new ArgumentNullException(nameof(algoFactory));

            using (var alg = algoFactory())
            {
                return inputs.Select(p => HashToHex(alg, p)).ToList();
            }
        }

        public static string ComputeHash(string input, Func<HashAlgorithm> algoFactory)
        {
            using (var alg = algoFactory())
                return HashToHex(alg, input);
        }

        // Friendly wrappers
        public static IReadOnlyList<string> Sha1(IEnumerable<string> inputs) => ComputeHashes(inputs, SHA1.Create);
        public static string Sha1(string input) => ComputeHash(input, SHA1.Create);

        public static IReadOnlyList<string> Sha256(IEnumerable<string> inputs) => ComputeHashes(inputs, SHA256.Create);
        public static string Sha256(string input) => ComputeHash(input, SHA256.Create);

        public static IReadOnlyList<string> Sha512(IEnumerable<string> inputs) => ComputeHashes(inputs, SHA512.Create);
        public static string Sha512(string input) => ComputeHash(input, SHA512.Create);

        public static IReadOnlyList<string> Md5(IEnumerable<string> inputs) => ComputeHashes(inputs, MD5.Create);
        public static string Md5(string input) => ComputeHash(input, MD5.Create);

        public static IReadOnlyList<string> Blake2b(IEnumerable<string> inputs) =>
            ComputeHashes(inputs, () => new Konscious.Security.Cryptography.HMACBlake2B(512));

        public static string Blake2b(string input) =>
            ComputeHash(input, () => new Konscious.Security.Cryptography.HMACBlake2B(512));

        // ---------- Difference percentage ----------------------------------
        public static double DifferencePercentage(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Strings must be the same length.");

            int diff = a.Zip(b, (c1, c2) => c1 != c2).Count(x => x);
            return diff * 100.0 / a.Length;
        }

        public static double DifferencePercentage(IReadOnlyList<string> list1, IReadOnlyList<string> list2)
        {
            if (list1.Count != list2.Count)
                throw new ArgumentException("Lists must be the same length.");

            int diff = 0, total = 0;
            for (int i = 0; i < list1.Count; i++)
            {
                var s1 = list1[i];
                var s2 = list2[i];
                if (s1.Length != s2.Length)
                    throw new ArgumentException($"Element {i} lengths differ.");

                diff += s1.Zip(s2, (c1, c2) => c1 != c2).Count(x => x);
                total += s1.Length;
            }
            return total == 0 ? 0 : diff * 100.0 / total;
        }
    }
}

