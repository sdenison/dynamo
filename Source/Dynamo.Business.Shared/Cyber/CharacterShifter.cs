using System;

namespace Dynamo.Business.Shared.Cyber
{
    public static class CharacterShifter
    {
        public static char ShiftRight(char c, int shift)
        {
            if (c >= 'a' && c <= 'z')
                return ShiftRight(c, shift, 'a');

            if (c >= 'A' && c <= 'Z')
                return ShiftRight(c, shift, 'A');

            return c;
        }

        private static char ShiftRight(char c, int shift, char beginningChar)
        {
            var index = c - beginningChar;
            return (char)(((index + shift) % 26) + beginningChar);
        }

        public static char ShiftLeft(char c, int shift)
        {
            if (c >= 'a' && c <= 'z')
                return ShiftLeft(c, shift, 'a', 'z');

            if (c >= 'A' && c <= 'Z')
                return ShiftLeft(c, shift, 'a', 'Z');

            return c;
        }

        public static char ShiftLeft(char c, int shift, char beginningChar, char endingChar)
        {
            var index = c - beginningChar;
            if ((index - shift) % 26 < 0)
            {
                return (char)(endingChar - Math.Abs((index - shift) % 26) + 1);
            }
            else
            {
                return (char)((beginningChar + (index - shift) % 26));
            }
        }
    }
}
