using Dynamo.Business.Shared.Cyber.Passwords;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class PasswordAnalyzerTests
    {
        [TestCase("!TMYC2025!", 30)]
        [TestCase("K9$pL!xQz&23", 43.02)]
        public void Can_get_entropy_numbers_for_example_2(string password, double expectedEntropy)
        {
            var example2Entropy = PasswordAnalyzer.GetPasswordEntropy(password);
            var tolerance = 0.001;
            Assert.That(example2Entropy, Is.EqualTo(expectedEntropy).Within(tolerance));
        }
    }
}
