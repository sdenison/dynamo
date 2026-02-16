using System.Numerics;

namespace dynamo.business.shared.cyber.passwords
{
    public static class PrettyGoodHash
    {
        public static string CreateHash(string input)
        {
            var bigInteger = GetBigInteger(input);
            return bigInteger.ToString();
        }

        public static BigInteger GetBigInteger(string input)
        {
            var bigInt = 0;
            foreach (var c in input.ToCharArray())
            {
                if (c % 4 == 0)
                    bigInt += (c - 1) * 30;
                else if (c % 2 == 0)
                    bigInt += c * 34;
                else if (c % 3 == 0)
                    bigInt += c * 5;
                else
                    bigInt += c * 2;
            }
            return bigInt;
        }
    }
}
