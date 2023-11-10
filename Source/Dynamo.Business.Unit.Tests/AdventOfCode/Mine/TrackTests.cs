using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Mine
{
    [TestFixture]
    public class TrackTests
    {
        [Test]
        public void Can_create_a_track()
        {
            var trackString = new string[]
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
