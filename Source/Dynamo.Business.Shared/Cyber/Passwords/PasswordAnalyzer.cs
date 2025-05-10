using System;
using System.Linq;

namespace Dynamo.Business.Shared.Cyber.Passwords
{
    public class PasswordAnalyzer
    {
        public static double GetPasswordEntropy(string password)
        {
            var numberOfSymbols = password.ToCharArray().Distinct().Count();
            var entropy = password.Count() * (Math.Log(numberOfSymbols) / Math.Log(2));
            return entropy;
        }
    }
}
