using System;

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

        public static int ConvertKey(string str)
        {
            return InvertDigits(AddInvertedParityBit(ParseByte(str)));
        }
    }
}
