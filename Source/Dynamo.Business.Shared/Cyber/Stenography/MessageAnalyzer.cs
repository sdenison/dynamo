using System;
using System.Collections.Generic;

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
                if (i == 0)
                    bytes.Add(currentString);
                else if (i % 7 == 0)
                {
                    bytes.Add(currentString);
                    currentString = string.Empty;
                }
            }

            return bytes;
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
