using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Security
{
    public class Passphrase
    {
        private readonly string[] _words;

        public bool IsValid()
        {
            var wordList = new SortedDictionary<string, string>();
            foreach (var word in _words)
            {
                if (wordList.Keys.Contains(word))
                    return false;
                wordList.Add(word, word);
            }
            return true;
        }

        public bool IsValidNoAnagrams()
        {
            var wordList = new SortedDictionary<string, List<string>>();
            foreach (var word in _words)
            {
                var deconstructedWord = new string(word.OrderBy(x => x).ToArray());
                if (wordList.Keys.Contains(deconstructedWord))
                {
                    wordList[deconstructedWord].Add(word);
                }
                else
                {
                    wordList.Add(deconstructedWord, new List<string>() { word });
                }
            }
            foreach (var word in wordList.Keys)
            {
                if (wordList[word].Count() > 1)
                    return false;
            }
            return true;
        }

        public Passphrase(string passphrase)
        {
            _words = passphrase.Split(' ');
        }
    }
}
