using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Warehouse
{
    public class BoxScanner
    {
        public BoxLabelType Scan(string input)
        {
            var charDictionary = new Dictionary<char, int>();

            foreach (var c in input)
            {
                if (charDictionary.ContainsKey(c))
                    charDictionary[c]++;
                else
                    charDictionary.Add(c, 1);
            }

            var hasExactlyTwo = false;
            var hasExactlyThree = false;

            foreach (var item in charDictionary.Values)
            {
                switch (item)
                {
                    case 2:
                        {
                            if (hasExactlyTwo == false)
                                hasExactlyTwo = true;
                            break;
                        }
                    case 3:
                        {
                            if (hasExactlyThree == false)
                                hasExactlyThree = true;
                            break;
                        }
                }
            }

            if (hasExactlyTwo && hasExactlyThree)
                return BoxLabelType.Both;
            if (hasExactlyTwo && !hasExactlyThree)
                return BoxLabelType.MatchTwo;
            if (hasExactlyThree && !hasExactlyTwo)
                return BoxLabelType.MatchThree;
            return BoxLabelType.None;
        }

        public int GetCheckSum(string[] inputStrings)
        {
            var twos = 0;
            var threes = 0;
            foreach (var input in inputStrings)
            {
                var result = Scan(input);
                switch (result)
                {
                    case BoxLabelType.Both:
                        twos++;
                        threes++;
                        break;
                    case BoxLabelType.MatchTwo:
                        twos++;
                        break;
                    case BoxLabelType.MatchThree:
                        threes++;
                        break;
                }
            }
            return twos * threes;
        }

        public string GetMatchingOffByOneLetter(string[] inputStrings)
        {
            foreach (var t in inputStrings)
                foreach (var t1 in inputStrings)
                    if (OffBy1(t, t1))
                        return GetMatchingPart(t, t1);
            throw new Exception("No off by one match found in inputStrings");
        }

        public bool OffBy1(string input1, string input2)
        {
            var offBy = input1.Where((t, i) => t != input2[i]).Count();
            return offBy == 1;
        }

        public string GetMatchingPart(string input1, string input2)
        {
            var matching = new StringBuilder();
            for (var i = 0; i < input1.Length; i++)
                if (input1[i] == input2[i])
                    matching.Append(input1[i]);
            return matching.ToString();
        }
    }
}
