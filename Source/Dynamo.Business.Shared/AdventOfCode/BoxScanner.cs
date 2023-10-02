using System;
using System.Collections.Generic;
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
                return BoxLabelType.ExactlyTwo;
            if (hasExactlyThree && !hasExactlyTwo)
                return BoxLabelType.ExactlyThree;
            return BoxLabelType.None;
        }
    }
}
