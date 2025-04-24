namespace Dynamo.Business.Shared.Cyber
{
    public static class CharacterShifter
    {
        public static char ShiftRight(char c, int shift) => Shift(c, shift);
        public static char ShiftRight(char c, char shift) => Shift(c, shift - GetBaseChar(c));

        public static char ShiftLeft(char c, int shift) => Shift(c, -shift);
        public static char ShiftLeft(char c, char shift) => Shift(c, -(shift - GetBaseChar(c)));

        private static char Shift(char c, int shift)
        {
            if (IsLower(c))
                return ShiftWithinAlphabet(c, shift, 'a');
            if (IsUpper(c))
                return ShiftWithinAlphabet(c, shift, 'A');

            return c;
        }

        private static char ShiftWithinAlphabet(char c, int shift, char baseChar)
        {
            int offset = c - baseChar;
            int normalizedShift = ((offset + shift) % 26 + 26) % 26; // handles negative shifts
            return (char)(baseChar + normalizedShift);
        }

        private static bool IsLower(char c) => c >= 'a' && c <= 'z';
        private static bool IsUpper(char c) => c >= 'A' && c <= 'Z';
        private static char GetBaseChar(char c) => IsLower(c) ? 'a' : IsUpper(c) ? 'A' : '\0';
    }
}
