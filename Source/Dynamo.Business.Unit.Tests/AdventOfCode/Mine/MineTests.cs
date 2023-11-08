using System;
using System.Linq;
using System.Xml.Serialization;
using Dynamo.Business.Shared.AdventOfCode.Mine;
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
                @"| | |  | v  |",
                @"\-+-/  \-+--/",
                @"  \------/   "
            };

            var mine = new Shared.AdventOfCode.Mine.Mine(mineLayout);
            Assert.IsNotNull(mine);

            //Trace the first track all the way around
            Assert.AreEqual(16, mine.Tracks[0].Sections.Count);
            Assert.AreEqual(TrackSectionType.TopLeft, mine.Tracks[0].Sections[0].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[1].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[2].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[3].Type);
            Assert.AreEqual(TrackSectionType.TopRight, mine.Tracks[0].Sections[4].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[5].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[0].Sections[6].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[0].Sections[6].IntersectionTrack);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[7].Type);
            Assert.AreEqual(TrackSectionType.LowerRight, mine.Tracks[0].Sections[8].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[9].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[0].Sections[10].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[0].Sections[10].IntersectionTrack);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[11].Type);
            Assert.AreEqual(TrackSectionType.LowerLeft, mine.Tracks[0].Sections[12].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[13].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[14].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[15].Type);

            //Second Track
            Assert.AreEqual(TrackSectionType.TopLeft, mine.Tracks[1].Sections[0].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[1].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[2].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[3].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[4].Type);
            Assert.AreEqual(TrackSectionType.TopRight, mine.Tracks[1].Sections[5].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[1].Sections[6].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[1].Sections[7].Type);
            Assert.AreEqual(TrackSectionType.LowerRight, mine.Tracks[1].Sections[8].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[9].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[10].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[1].Sections[11].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[1].Sections[11].IntersectionTrack);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[12].Type);
            Assert.AreEqual(TrackSectionType.LowerLeft, mine.Tracks[1].Sections[13].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[1].Sections[14].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[1].Sections[15].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[1].Sections[15].IntersectionTrack);
        }

        [Test]
        public void Can_move_cart_along_track()
        {
            var mineLayout = new string[]
            {
                @"/->-\        ",
                @"|   |  /----\",
                @"| /-+--+-\  |",
                @"| | |  | v  |",
                @"\-+-/  \-+--/",
                @"  \------/   "
            };
            var mine = new Shared.AdventOfCode.Mine.Mine(mineLayout);

            var carts = mine.GetCarts();
            Assert.AreEqual(2, carts.Count);
            Assert.AreEqual(2, carts[0].Point.X);
            Assert.AreEqual(0, carts[0].Point.Y);
            Assert.AreEqual(3, carts[1].Point.Y);
            Assert.AreEqual(9, carts[1].Point.X);
        }
    }
}
