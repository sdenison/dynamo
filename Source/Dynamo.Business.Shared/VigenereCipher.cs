using Dynamo.Business.Shared.Cyber;
using System.Text;

namespace Dynamo.Business.Shared
{
    public static class VigenereCipher
    {
        public static string Encrypt(string encrypted, string keyWord)
        {
            var returnString = new StringBuilder();
            var shiftIndex = 0;
            var shift = keyWord[shiftIndex];

            foreach (char c in encrypted)
            {
                var encrptedChar = CharacterShifter.ShiftRight(c, shift);
                if (encrptedChar != c)
                {
                    returnString.Append(encrptedChar);
                    shiftIndex++;
                    shiftIndex = shiftIndex % keyWord.Length;
                    shift = keyWord[shiftIndex];
                }
                else
                {
                    returnString.Append(c);
                }
            }
            return returnString.ToString();
        }

        public static string Decrypt(string encrypted, string keyWord)
        {
            var returnString = new StringBuilder();
            var shiftIndex = 0;
            var shift = keyWord[shiftIndex];

            foreach (char c in encrypted)
            {
                if (!(c >= 'a' && c <= 'z') && !(c >= 'A' && c <= 'Z'))
                {
                    returnString.Append(c);
                }
                else
                {
                    var encrptedChar = CharacterShifter.ShiftLeft(c, shift);
                    returnString.Append(encrptedChar);
                    shiftIndex++;
                    shiftIndex = shiftIndex % keyWord.Length;
                    shift = keyWord[shiftIndex];
                }

                //if (encrptedChar != c)
                //{
                //    returnString.Append(encrptedChar);
                //    shiftIndex++;
                //    shiftIndex = shiftIndex % keyWord.Length;
                //    shift = keyWord[shiftIndex];
                //}
                //else
                //{
                //    returnString.Append(c);
                //}
            }
            return returnString.ToString();
        }


    }
}
