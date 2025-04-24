using System.Text;

namespace Dynamo.Business.Shared.Cyber
{
    public static class VigenereCipher
    {
        public static string Encrypt(string input, string keyword)
            => Transform(input, keyword, isDecrypt: false);

        public static string Decrypt(string input, string keyword)
            => Transform(input, keyword, isDecrypt: true);

        private static string Transform(string input, string keyword, bool isDecrypt)
        {
            var result = new StringBuilder();
            int shiftIndex = 0;

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char shift = keyword[shiftIndex % keyword.Length];
                    char transformedChar = isDecrypt
                        ? CharacterShifter.ShiftLeft(c, shift)
                        : CharacterShifter.ShiftRight(c, shift);

                    result.Append(transformedChar);
                    shiftIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
