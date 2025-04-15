using Dynamo.Business.Shared.Cyber;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    [TestFixture]
    public class CharacterShifterTests
    {
        [Test]
        public void Play_with_characters()
        {
            var a = 'a';
            Assert.That((int)a, Is.EqualTo(97));
            var z = 'z';
            Assert.That((int)z, Is.EqualTo(122));
            var A = 'A';
            Assert.That((int)A, Is.EqualTo(65));
            var Z = 'Z';
            Assert.That((int)Z, Is.EqualTo(90));
        }

        [Test]
        public void Shifting_characters_should_work_for_lower_case()
        {
            var a = 'a';
            var b = CharacterShifter.Shift(a, 1);
            Assert.That(b, Is.EqualTo(b));
            var z = 'z';
            var d = CharacterShifter.Shift(z, 4);
            Assert.That(d, Is.EqualTo('d'));
            var e = CharacterShifter.Shift(z, 31);
            Assert.That(e, Is.EqualTo('e'));
        }
    }
}
