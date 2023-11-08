using System;
using System.Linq;
using System.Linq.Expressions;
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
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[0].Sections[6].IntersectingTrackSection.Track);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[0].Sections[7].Type);
            Assert.AreEqual(TrackSectionType.LowerRight, mine.Tracks[0].Sections[8].Type);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[0].Sections[9].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[0].Sections[10].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[0].Sections[10].IntersectingTrackSection.Track);
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
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[1].Sections[11].IntersectingTrackSection.Track);
            Assert.AreEqual(TrackSectionType.Horizontal, mine.Tracks[1].Sections[12].Type);
            Assert.AreEqual(TrackSectionType.LowerLeft, mine.Tracks[1].Sections[13].Type);
            Assert.AreEqual(TrackSectionType.Vertical, mine.Tracks[1].Sections[14].Type);
            Assert.AreEqual(TrackSectionType.Intersection, mine.Tracks[1].Sections[15].Type);
            Assert.AreEqual(mine.Tracks[2], mine.Tracks[1].Sections[15].IntersectingTrackSection.Track);
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

            mine.MoveBy1(carts[0]);
            Assert.AreEqual(3, carts[0].Point.X);
            mine.MoveBy1(carts[0]);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(mine.Tracks[0], carts[0].TrackSection.Track);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(mine.Tracks[2], carts[0].TrackSection.Track);
            Assert.AreEqual(Rotation.Clockwise, carts[0].Rotation);
            Assert.AreEqual(4, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(5, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(6, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(7, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            Assert.AreEqual(mine.Tracks[2], carts[0].TrackSection.Track);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(8, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(9, carts[0].Point.X);
            Assert.AreEqual(2, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(9, carts[0].Point.X);
            Assert.AreEqual(3, carts[0].Point.Y);
            mine.MoveBy1(carts[0]);
            Assert.AreEqual(9, carts[0].Point.X);
            Assert.AreEqual(4, carts[0].Point.Y);
            Assert.AreEqual(mine.Tracks[1], carts[0].TrackSection.Track);
        }

        [Test]
        public void Can_move_cart_along_track2()
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

            var cart = mine.GetCarts().First();
            Assert.AreEqual(2, cart.Point.X);
            Assert.AreEqual(0, cart.Point.Y);

            cart.MoveBy1();
            Assert.AreEqual(3, cart.Point.X);
            cart.MoveBy1();
            cart.MoveBy1();
            Assert.AreEqual(mine.Tracks[0], cart.TrackSection.Track);
            cart.MoveBy1();
            Assert.AreEqual(mine.Tracks[2], cart.TrackSection.Track);
            Assert.AreEqual(Rotation.Clockwise, cart.Rotation);
            Assert.AreEqual(4, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(5, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(6, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(7, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            Assert.AreEqual(mine.Tracks[2], cart.TrackSection.Track);
            cart.MoveBy1();
            Assert.AreEqual(8, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(9, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(9, cart.Point.X);
            Assert.AreEqual(3, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(9, cart.Point.X);
            Assert.AreEqual(4, cart.Point.Y);
            Assert.AreEqual(mine.Tracks[1], cart.TrackSection.Track);
            cart.MoveBy1();
            Assert.AreEqual(8, cart.Point.X);
            Assert.AreEqual(4, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(7, cart.Point.X);
            Assert.AreEqual(4, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(7, cart.Point.X);
            Assert.AreEqual(3, cart.Point.Y);
            cart.MoveBy1();
            Assert.AreEqual(7, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);
            Assert.AreEqual(mine.Tracks[2], cart.TrackSection.Track);
            cart.MoveBy1();
            Assert.AreEqual(6, cart.Point.X);
            Assert.AreEqual(2, cart.Point.Y);

            for (var i = 0; i < 10000; i++)
            {
                try
                {
                    cart.MoveBy1();
                }
                catch (Exception ex)
                {
                    var x = i;
                }
            }
        }
    }
}
