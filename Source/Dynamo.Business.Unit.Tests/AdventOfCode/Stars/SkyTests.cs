using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Assert.That(31, Is.EqualTo(sky.Points.Count));
            var display = sky.Display();
            Assert.That(16, Is.EqualTo(display.Count));
            Assert.That(display, Is.EqualTo(GetSecond0()));
            sky.Step();
            Assert.That(sky.Display(), Is.EqualTo(GetSecond1()));
            sky.Step();
            Assert.That(sky.Display(), Is.EqualTo(GetSecond2()));
            sky.Step();
            Assert.That(sky.Display(), Is.EqualTo(GetSecond3()));
        }

        [Test]
        public void Can_find_smallest_area()
        {
            var sky = new Sky(GetTestData());
            var stepWithMessage = sky.FindStepWithMessage();
            Assert.That(3, Is.EqualTo(stepWithMessage));
        }

        [Test]
        public void Can_solve_day_10_part_1()
        {
            var sky = new Sky(TestDataProvider.GetPuzzleInput());
            var stepWithMessage = sky.FindStepWithMessage();
            Assert.That(10243, Is.EqualTo(stepWithMessage));
            sky.TakeSteps(stepWithMessage);
            var display = sky.DisplayCondensed();
            using (StreamWriter outputFile = new StreamWriter("d:\\temp\\day10output.txt"))
            {
                foreach (var line in display)
                {
                    outputFile.WriteLine(line);
                }
            }
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

        public List<string> GetSecond2()
        {
            string[] display = new[]
            {
                "......................",
                "......................",
                "......................",
                "..............#.......",
                "....#..#...####..#....",
                "......................",
                "........#....#........",
                "......#.#.............",
                ".......#...#..........",
                ".......#..#..#.#......",
                "....#....#.#..........",
                ".....#...#...##.#.....",
                "........#.............",
                "......................",
                "......................",
                "......................",
            };
            return display.ToList();
        }

        public List<string> GetSecond3()
        {
            string[] display = new[]
            {
                "......................",
                "......................",
                "......................",
                "......................",
                "......#...#..###......",
                "......#...#...#.......",
                "......#...#...#.......",
                "......#####...#.......",
                "......#...#...#.......",
                "......#...#...#.......",
                "......#...#...#.......",
                "......#...#..###......",
                "......................",
                "......................",
                "......................",
                "......................",
            };
            return display.ToList();
        }
    }
}

