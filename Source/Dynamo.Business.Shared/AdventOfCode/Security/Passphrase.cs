using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Security
{
    public class Passphrase
    {
        public readonly string[] Words;
        public Dictionary<string, int> WordCount;
        public Dictionary<string, List<string>> Anagrams;

        public bool IsValid()
        {
            foreach (var word in WordCount.Keys)
            {
                if (WordCount[word] > 1)
                    return false;
            }
            return true;
        }

        public bool IsValidNoAnagrams()
        {
            foreach (var word in Anagrams.Keys)
            {
                if (Anagrams[word].Count() > 1)
                    return false;
            }
            return true;
        }

        public Passphrase(string passphrase)
        {
            Words = passphrase.Split(' ');
            WordCount = new Dictionary<string, int>();
            Anagrams = new Dictionary<string, List<string>>();
            foreach (var word in Words)
            {
                if (WordCount.Keys.Contains(word))
                {
                    WordCount[word] += 1;
                }
                else
                {
                    WordCount.Add(word, 1);
                }
                var deconstructedWord = new string(word.OrderBy(x => x).ToArray());
                if (Anagrams.Keys.Contains(deconstructedWord))
                {
                    Anagrams[deconstructedWord].Add(word);
                }
                else
                {
                    Anagrams.Add(deconstructedWord, new List<string>() { word });
                }
            }
        }
    }
}
