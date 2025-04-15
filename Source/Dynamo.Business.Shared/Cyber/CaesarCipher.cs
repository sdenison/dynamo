using System.Text;

namespace Dynamo.Business.Shared.Cyber
{
    public static class CaesarCipher
    {
        public static string Decrypt(string encrypted, int shift)
        {
            var returnString = new StringBuilder();
            foreach (char c in encrypted)
            {
                returnString.Append(CharacterShifter.ShiftLeft(c, shift));
            }
            return returnString.ToString();
        }

        public static string Encrypt(string encrypted, int shift)
        {
            var returnString = new StringBuilder();
            foreach (char c in encrypted)
            {
                returnString.Append(CharacterShifter.ShiftRight(c, shift));
            }
            return returnString.ToString();
        }

        public static string GetInbetweenWords(string encrypted, int shift, string bookEndWord)
        {
            var decrypted = Decrypt(encrypted, shift);
            var firstIndex = decrypted.IndexOf(bookEndWord);
            var secondIndex = decrypted.IndexOf(bookEndWord, firstIndex + bookEndWord.Length);
            int start = firstIndex + bookEndWord.Length;
            return decrypted.Substring(start, secondIndex - start).Trim();
        }
    }
}
