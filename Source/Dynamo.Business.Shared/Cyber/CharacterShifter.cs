namespace Dynamo.Business.Shared.Cyber
{
    public static class CharacterShifter
    {
        public static char Shift(char c, int shift)
        {
            if (c >= 'a' && c <= 'z')
                return Shift(c, shift, 'a');

            if (c >= 'A' && c <= 'Z')
                return Shift(c, shift, 'A');

            return c;
        }

        private static char Shift(char c, int shift, char beginnningChar)
        {
            var index = c - beginnningChar;
            return (char)(((index + shift) % 26) + beginnningChar);
        }
    }
}
