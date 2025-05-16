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

        private bool IsPunctuation(char c)
        {
            foreach (var p in _punctuation)
                if (p == c)
                    return true;
            return false;
        }
    }
}
