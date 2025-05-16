using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.Cyber.Stenography
{
    public class MessageAnalyzer
    {
        private char[] _punctuation = new char[]
            {
                '.',
                ',',
                '!',
                '?',
                ';',
                ':'
            };


        public List<string> GetSpacesAfterPunctuation(string input)
        {
            var list = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '!')
                {
                    var xxxx = "got here";
                }
                if (IsPunctuation(input[i]) && i + 1 < input.Length && input[i + 1] == ' ')
                {
                    // check for two spaces safely
                    list.Add(i + 2 < input.Length && input[i + 2] == ' ' ? "  " : " ");
                }
            }

            return list;
        }

        public static byte ParseByte(string str)
        {
            return Convert.ToByte(str, 2);
        }

        public static string GetStringFromOnesAndZeroString(string onesAndZeros, int offset)
        {
            var bytes = new List<byte>();
            var b = string.Empty;
            for (var i = offset; i < onesAndZeros.Length; i++)
            {
                b = b + onesAndZeros[i];
                if (b.Length == 8)
                {
                    bytes.Add(ParseByte(b));
                    b = string.Empty;
                }
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }

        public List<string> GetBytesFromSpaces(List<string> spaces)
        {
            var bytes = new List<string>();
            var currentString = string.Empty;

            for (int i = 0; i < spaces.Count; i++)
            {
                if (spaces[i].Length == 1)
                    currentString = currentString + '0';

                if (spaces[i].Length == 2)
                    currentString = currentString + '1';

                if (currentString.Length == 8)
                {
                    bytes.Add(currentString);
                    currentString = string.Empty;
                }
            }

            return bytes;
        }

        public static List<string> GetBytesFromCase(string text, bool padIncompleteByte = false)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));

            var bytes = new List<string>(text.Length / 8 + 1);
            var current = new StringBuilder(8);              // collects the 8 bits of the current “byte”

            foreach (char c in text)
            {
                // Only letters contribute bits — skip anything else.
                if (!char.IsLetter(c)) continue;

                current.Append(char.IsUpper(c) ? '1' : '0');

                if (current.Length == 8)
                {
                    bytes.Add(current.ToString());
                    current.Clear();                         // start a new byte
                }
            }

            // Handle a trailing partial byte if any bits are left over
            if (current.Length > 0)
            {
                if (padIncompleteByte)
                    current.Append('0', 8 - current.Length); // pad to 8 bits

                bytes.Add(current.ToString());
            }

            return bytes;
        }

        public static string GetBytesFromLeastSignificantDights(byte[] bytes)
        {
            var onesAndZeros = new StringBuilder();
            foreach (var b in bytes)
            {
                // If it's even the LSB should be 0
                if (b % 2 == 0)
                    onesAndZeros.Append("0");
                else
                    onesAndZeros.Append("1");
            }
            return onesAndZeros.ToString();
        }

        private bool IsPunctuation(char c)
        {
            foreach (var p in _punctuation)
                if (p == c)
                    return true;
            return false;
        }
    }
}
