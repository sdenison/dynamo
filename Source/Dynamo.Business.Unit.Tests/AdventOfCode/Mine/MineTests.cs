using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
            Assert.That(mine, Is.Not.Null);

            //Trace the first track all the way around
            Assert.That(16, Is.EqualTo(mine.Tracks[0].Sections.Count));
            Assert.That(TrackSectionType.TopLeft, Is.EqualTo(mine.Tracks[0].Sections[0].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[0].Sections[1].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[0].Sections[2].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[0].Sections[3].Type));
            Assert.That(TrackSectionType.TopRight, Is.EqualTo(mine.Tracks[0].Sections[4].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[0].Sections[5].Type));
            Assert.That(TrackSectionType.Intersection, Is.EqualTo(mine.Tracks[0].Sections[6].Type));
            Assert.That(mine.Tracks[2], Is.EqualTo(mine.Tracks[0].Sections[6].IntersectingTrackSection.Track));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[0].Sections[7].Type));
            Assert.That(TrackSectionType.LowerRight, Is.EqualTo(mine.Tracks[0].Sections[8].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[0].Sections[9].Type));
            Assert.That(TrackSectionType.Intersection, Is.EqualTo(mine.Tracks[0].Sections[10].Type));
            Assert.That(mine.Tracks[2], Is.EqualTo(mine.Tracks[0].Sections[10].IntersectingTrackSection.Track));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[0].Sections[11].Type));
            Assert.That(TrackSectionType.LowerLeft, Is.EqualTo(mine.Tracks[0].Sections[12].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[0].Sections[13].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[0].Sections[14].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[0].Sections[15].Type));

            //Second Track
            Assert.That(TrackSectionType.TopLeft, Is.EqualTo(mine.Tracks[1].Sections[0].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[1].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[2].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[3].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[4].Type));
            Assert.That(TrackSectionType.TopRight, Is.EqualTo(mine.Tracks[1].Sections[5].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[1].Sections[6].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[1].Sections[7].Type));
            Assert.That(TrackSectionType.LowerRight, Is.EqualTo(mine.Tracks[1].Sections[8].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[9].Type));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[10].Type));
            Assert.That(TrackSectionType.Intersection, Is.EqualTo(mine.Tracks[1].Sections[11].Type));
            Assert.That(mine.Tracks[2], Is.EqualTo(mine.Tracks[1].Sections[11].IntersectingTrackSection.Track));
            Assert.That(TrackSectionType.Horizontal, Is.EqualTo(mine.Tracks[1].Sections[12].Type));
            Assert.That(TrackSectionType.LowerLeft, Is.EqualTo(mine.Tracks[1].Sections[13].Type));
            Assert.That(TrackSectionType.Vertical, Is.EqualTo(mine.Tracks[1].Sections[14].Type));
            Assert.That(TrackSectionType.Intersection, Is.EqualTo(mine.Tracks[1].Sections[15].Type));
            Assert.That(mine.Tracks[2], Is.EqualTo(mine.Tracks[1].Sections[15].IntersectingTrackSection.Track));
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
            Assert.That(2, Is.EqualTo(cart.Point.X));
            Assert.That(0, Is.EqualTo(cart.Point.Y));

            cart.Step();
            Assert.That(3, Is.EqualTo(cart.Point.X));
            cart.Step();
            cart.Step();
            Assert.That(mine.Tracks[0], Is.EqualTo(cart.TrackSection.Track));
            cart.Step();
            Assert.That(mine.Tracks[2], Is.EqualTo(cart.TrackSection.Track));
            Assert.That(Rotation.Clockwise, Is.EqualTo(cart.Rotation));
            Assert.That(4, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(5, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(6, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(7, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            Assert.That(mine.Tracks[2], Is.EqualTo(cart.TrackSection.Track));
            cart.Step();
            Assert.That(8, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(9, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(9, Is.EqualTo(cart.Point.X));
            Assert.That(3, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(9, Is.EqualTo(cart.Point.X));
            Assert.That(4, Is.EqualTo(cart.Point.Y));
            Assert.That(mine.Tracks[1], Is.EqualTo(cart.TrackSection.Track));
            cart.Step();
            Assert.That(8, Is.EqualTo(cart.Point.X));
            Assert.That(4, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(7, Is.EqualTo(cart.Point.X));
            Assert.That(4, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(7, Is.EqualTo(cart.Point.X));
            Assert.That(3, Is.EqualTo(cart.Point.Y));
            cart.Step();
            Assert.That(7, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
            Assert.That(mine.Tracks[2], Is.EqualTo(cart.TrackSection.Track));
            cart.Step();
            Assert.That(6, Is.EqualTo(cart.Point.X));
            Assert.That(2, Is.EqualTo(cart.Point.Y));
        }

        [Test]
        public void Can_get_first_collision_for_test_data_given()
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
            var firstCollision = mine.GetFirstCollision();
            Assert.That(7, Is.EqualTo(firstCollision.X));
            Assert.That(3, Is.EqualTo(firstCollision.Y));
            var numbers = TestDataProvider.GetPuzzleData();
        }

        [Test]
        public void Can_get_day_13_part_1_puzzle_answer()
        {
            var mineLayout = TestDataProvider.GetFullPuzzleInput();
            var mine = new Shared.AdventOfCode.Mine.Mine(mineLayout);
            var firstCollision = mine.GetFirstCollision();
            Assert.That(50, Is.EqualTo(firstCollision.X));
            Assert.That(54, Is.EqualTo(firstCollision.Y));
        }

        [Test]
        public void Can_get_last_cart_location_for_example()
        {
            var mineLayout = new string[]
            {
                @"/>-<\  ",
                @"|   |  ",
                @"| /<+-\",
                @"| | | v",
                @"\>+</ |",
                @"  |   ^",
                @"  \<->/"
            };
            var mine = new Shared.AdventOfCode.Mine.Mine(mineLayout);
            var lastCart = mine.GetLastCart();
            Assert.That(6, Is.EqualTo(lastCart.Point.X));
            Assert.That(4, Is.EqualTo(lastCart.Point.Y));
        }

        [Test]
        public void Can_get_day_13_part_2_puzzle_answer()
        {
            var mineLayout = TestDataProvider.GetFullPuzzleInput();
            var mine = new Shared.AdventOfCode.Mine.Mine(mineLayout);
            var lastCart = mine.GetLastCart();
            Assert.That(50, Is.EqualTo(lastCart.Point.X));
            Assert.That(100, Is.EqualTo(lastCart.Point.Y));
        }
    }
}

