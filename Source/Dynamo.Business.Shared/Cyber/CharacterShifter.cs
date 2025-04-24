namespace Dynamo.Business.Shared.Cyber
{
    public static class CharacterShifter
    {
        // Caesar-style shifting
        public static char Shift(char c, int shift)
        {
            if (char.IsUpper(c))
            {
                return (char)('A' + (26 + (c - 'A' + shift) % 26) % 26);
            }
            else if (char.IsLower(c))
            {
                return (char)('a' + (26 + (c - 'a' + shift) % 26) % 26);
            }
            else
            {
                return c;
            }
        }

        public static char ShiftRight(char c, int shift) => Shift(c, shift);
        public static char ShiftLeft(char c, int shift) => Shift(c, -shift);

        // Vigenère-style shifting using a key character
        public static char ShiftRight(char c, char key)
        {
            int shift = char.IsUpper(key) ? key - 'A' : key - 'a';
            return Shift(c, shift);
        }

        public static char ShiftLeft(char c, char key)
        {
            int shift = char.IsUpper(key) ? key - 'A' : key - 'a';
            return Shift(c, -shift);
        }
    }
}

