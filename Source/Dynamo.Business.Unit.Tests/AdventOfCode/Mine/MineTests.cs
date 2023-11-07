using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Mine
{
    [TestFixture]
    public class MineTests
    {
        [Test]
        public void Can_create_a_mine()
        {
            var mineLayout = new string[]
            {
                @"/->-\        ",
                @"|   |  /----\",
                @"| /-+--+-\  |",
                @"| | |  | v  | ",
                @"\-+-/  \-+--/",
                @"  \------/"
            };
        }
    }
}
