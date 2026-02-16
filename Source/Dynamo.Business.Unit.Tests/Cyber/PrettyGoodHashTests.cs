using dynamo.business.shared.cyber.passwords;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class PrettyGoodHashTests
    {
        [Test]
        public void Can_create_PrettyGoodHash()
        {
            // Make sure the hashes of these similar passwords are different
            var passwordA = "ThisIsFakePasswordA";
            var passwordB = "ThisIsFakePasswordB";

            var hashedPasswordA = PrettyGoodHash.CreateHash(passwordA);
            Assert.That(hashedPasswordA, Is.EqualTo("20494"));

            var hashedPasswordB = PrettyGoodHash.CreateHash(passwordB);
            Assert.That(hashedPasswordB, Is.EqualTo("22608"));
        }


        private static string GetOneBigString(string fileName)
        {
            var stream = FileGetter.GetMemoryStreamFromFile(fileName);
            var sb = new StringBuilder();

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    sb.Append(line.Trim());
            }
            return sb.ToString();
        }
    }
}
