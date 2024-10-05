using Dynamo.Business.Shared.AdventOfCode.Security;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Security
{
    [TestFixture]
    public class PassphraseTests
    {
        [TestCase("aa bb cc dd ee", true)]
        [TestCase("aa bb cc dd aa", false)]
        [TestCase("aa bb cc dd aaa", true)]
        public void Can_determine_if_Passphrase_is_valid(string passphraseString, bool isValid)
        {
            var passphrase = new Passphrase(passphraseString);
            Assert.That(passphrase.IsValid, Is.EqualTo(isValid));
        }

        [TestCase("abcde fghij", true)]
        [TestCase("abcde xyz ecdab", false)]
        [TestCase("a ab abc abd abf abj", true)]
        [TestCase("iiii oiii ooii oooi oooo", true)]
        [TestCase("oiii ioii iioi iiio", false)]
        public void Can_determine_if_Passphrase_is_valid_no_anagrams(string passphraseString, bool isValid)
        {
            var passphrase = new Passphrase(passphraseString);
            Assert.That(passphrase.IsValidNoAnagrams, Is.EqualTo(isValid));
        }
    }
}
