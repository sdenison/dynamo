using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode
{
    public class BoxScanner
    {
        public BoxLabelType Scan(string input)
        {
            var charDictionary = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (charDictionary.ContainsKey(c))
                {
                    charDictionary[c]++;
                }
                else
                {
                    charDictionary.Add(c, 1);
                }
            }

            bool hasExactlyTwo = false;
            bool hasExactlyThree = false;

            foreach (var item in charDictionary.Values)
            {
                if (item == 2)
                {
                    if (hasExactlyTwo == false)
                    {
                        hasExactlyTwo = true;
                    }
                }

                if (item == 3)
                {
                    if (hasExactlyThree == false)
                    {
                        hasExactlyThree = true;
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
            int twos = 0;
            int threes = 0;
            foreach (var input in inputStrings)
            {
                var result = Scan(input);
                if (result == BoxLabelType.Both)
                {
                    twos++;
                    threes++;
                }
                if (result == BoxLabelType.MatchTwo)
                    twos++;
                if (result == BoxLabelType.MatchThree)
                    threes++;
            }
            return twos * threes;
        }
    }
}
