namespace Dynamo.Business.Shared.Cyber
{
    public static class CharacterShifter
    {
        public static char Shift(char c, int shift)
        {
            if (c >= 'a' && c <= 'z')
                return ShiftLower(c, shift);

            if (c >= 'A' && c <= 'Z')
                return ShiftLower(c, shift);

            return c;
        }

        private static char ShiftLower(char c, int shift)
        {
            var index = (int)c - 97;
            return (char)(((index + shift) % 26) + 97);
        }

    }
}
