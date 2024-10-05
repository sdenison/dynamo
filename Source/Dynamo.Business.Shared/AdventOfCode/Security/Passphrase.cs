using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Security
{
    public class Passphrase
    {
        private readonly string[] _words;

        public bool IsValid()
        {
            var wordList = new List<string>();
            foreach (var word in _words)
            {
                if (wordList.Contains(word))
                    return false;
                wordList.Add(word);
            }
            return true;
        }


        public Passphrase(string passphrase)
        {
            _words = passphrase.Split(' ');
        }
    }
}
