using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Cyber.Passwords
{
    public class PasswordAnalyzer
    {
        public static double GetPasswordEntropy(string password)
        {
            var numberOfSymbols = password.ToCharArray().Distinct().Count();
            var entropy = password.Count() * (Math.Log(numberOfSymbols) / Math.Log(2));
            return entropy;
        }

        public static List<string> ComputeSha1Hash(List<string> inputs)
        {
            return inputs.Select(x => ComputeSha1Hash(x)).ToList();
        }

        public static string ComputeSha1Hash(string input)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha1.ComputeHash(inputBytes);
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }

        public static List<string> ComputeSha256Hash(List<string> inputs)
        {
            return inputs.Select(x => ComputeSha256Hash(x)).ToList();
        }

        public static string ComputeSha256Hash(string input)
        {
            using (var sha1 = System.Security.Cryptography.SHA256.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha1.ComputeHash(inputBytes);
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }

        public static double GetDifferencePercentage(string input1, string input2)
        {
            if (input1.Length != input2.Length)
                throw new ArgumentException($"Parameters input1 and input2 must be the same length: {input1}   {input2}");
            var differentChars = 0;
            for (var i = 0; i < input2.Length; i++)
            {
                if (input1[i] != input2[i])
                    differentChars++;
            }
            return ((double)differentChars / input1.Length) * 100;
        }

        public static double GetDifferencePercentage(List<string> input1List, List<string> input2List)
        {
            if (input1List.Count != input2List.Count)
                throw new ArgumentException($"Parameters input1List and input2List must be the same length");
            double differencePercentage = 0;
            for (var i = 0; i < input1List.Count; i++)
            {
                differencePercentage += GetDifferencePercentage(input1List[i], input2List[i]);
            }
            return (double)differencePercentage / input1List.Count;
        }
    }
}
