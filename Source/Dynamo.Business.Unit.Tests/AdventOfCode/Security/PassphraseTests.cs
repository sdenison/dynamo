using NUnit.Framework;
using Dynamo.Business.Shared.AdventOfCode.Security;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Security
{
    [TestFixture]
    public class PassphraseTests
    {
        [TestCase("aa bb cc dd ee", true)]
        [TestCase("aa bb cc dd aa", false)]
        [TestCase("aa bb cc dd aaa", true)]
        public void Can_create_determine_if_Passphrase_is_valid(string passphraseString, bool isValid)
        {
            var passphrase = new Passphrase(passphraseString);
            Assert.That(passphrase.IsValid, Is.EqualTo(isValid));
        }
    }
}
