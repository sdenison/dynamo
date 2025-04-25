using System;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.Cyber.Scanner
{
    public class KeyfileReader
    {
        public static byte ParseByte(string str)
        {
            return Convert.ToByte(str, 2);
        }

        public static byte AddInvertedParityBit(byte b)
        {
            return (byte)((b * 2) + 1);
        }

        public static byte InvertDigits(byte b)
        {
            return (byte)(b ^ 255);
        }

        public static byte ConvertKey(string str)
        {
            return InvertDigits(AddInvertedParityBit(ParseByte(str)));
        }

        public static byte[] ConvertKeys(string keys)
        {
            return keys.Split(' ').Select(ConvertKey).ToArray();
        }

        public static string XorUtf8String(string input, byte[] key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] outputBytes = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i++)
            {
                outputBytes[i] = (byte)(inputBytes[i] ^ key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(outputBytes);
        }
    }
}
