using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.AdventOfCode.Stars;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Stars
{
    [TestFixture]
    public class SkyTests
    {
        [Test]
        public void Can_display_sky()
        {
            var sky = new Sky(GetTestData());
            Assert.AreEqual(31, sky.Points.Count);
            var display = sky.Display();
            Assert.AreEqual(16, display.Count);
            Assert.AreEqual(display, GetSecond0());
            sky.Step();
            Assert.AreEqual(sky.Display(), GetSecond1());
        }

        private bool StringListsMatch(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (var x = 0; x < list1.Count; x++)
                if (!list1[x].Equals(list2[x]))
                    return false;
            return true;
        }

        public string[] GetTestData()
        {
            return new[]{
                "position=< 9,  1> velocity=< 0,  2>",
                "position=< 7,  0> velocity=<-1,  0>",
                "position=< 3, -2> velocity=<-1,  1>",
                "position=< 6, 10> velocity=<-2, -1>",
                "position=< 2, -4> velocity=< 2,  2>",
                "position=<-6, 10> velocity=< 2, -2>",
                "position=< 1,  8> velocity=< 1, -1>",
                "position=< 1,  7> velocity=< 1,  0>",
                "position=<-3, 11> velocity=< 1, -2>",
                "position=< 7,  6> velocity=<-1, -1>",
                "position=<-2,  3> velocity=< 1,  0>",
                "position=<-4,  3> velocity=< 2,  0>",
                "position=<10, -3> velocity=<-1,  1>",
                "position=< 5, 11> velocity=< 1, -2>",
                "position=< 4,  7> velocity=< 0, -1>",
                "position=< 8, -2> velocity=< 0,  1>",
                "position=<15,  0> velocity=<-2,  0>",
                "position=< 1,  6> velocity=< 1,  0>",
                "position=< 8,  9> velocity=< 0, -1>",
                "position=< 3,  3> velocity=<-1,  1>",
                "position=< 0,  5> velocity=< 0, -1>",
                "position=<-2,  2> velocity=< 2,  0>",
                "position=< 5, -2> velocity=< 1,  2>",
                "position=< 1,  4> velocity=< 2,  1>",
                "position=<-2,  7> velocity=< 2, -2>",
                "position=< 3,  6> velocity=<-1, -1>",
                "position=< 5,  0> velocity=< 1,  0>",
                "position=<-6,  0> velocity=< 2,  0>",
                "position=< 5,  9> velocity=< 1, -2>",
                "position=<14,  7> velocity=<-2,  0>",
                "position=<-3,  6> velocity=< 2, -1>"
            };
        }

        public List<string> GetSecond0()
        {
            string[] display = new[]
            {
                "........#.............",
                "................#.....",
                ".........#.#..#.......",
                "......................",
                "#..........#.#.......#",
                "...............#......",
                "....#.................",
                "..#.#....#............",
                ".......#..............",
                "......#...............",
                "...#...#.#...#........",
                "....#..#..#.........#.",
                ".......#..............",
                "...........#..#.......",
                "#...........#.........",
                "...#.......#..........",
            };
            return display.ToList();
        }

        public List<string> GetSecond1()
        {
            string[] display = new[]
            {
                "......................",
                "......................",
                "..........#....#......",
                "........#.....#.......",
                "..#.........#......#..",
                "......................",
                "......#...............",
                "....##.........#......",
                "......#.#.............",
                ".....##.##..#.........",
                "........#.#...........",
                "........#...#.....#...",
                "..#...........#.......",
                "....#.....#.#.........",
                "......................",
                "......................",
            };
            return display.ToList();
        }
    }
}
