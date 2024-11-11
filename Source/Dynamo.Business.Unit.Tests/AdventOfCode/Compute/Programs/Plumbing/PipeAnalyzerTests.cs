using Dynamo.Business.Shared.AdventOfCode.Compute.Programs.Plumber;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Programs.Plumbing
{
    [TestFixture]
    public class PipeAnalyzerTests
    {
        [Test]
        public void Can_create_PipeAnalyzer()
        {
            var input = GetExampleInput();
            var pipeAnalyzer = new PipeAnalyzer(input);
            Assert.That(pipeAnalyzer.Programs.Single(x => x.ProgramId == 4).Neighbors.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_get_all_associated_Programs()
        {
            var input = GetExampleInput();
            var pipeAnalyzer = new PipeAnalyzer(input);
            var connectedPrograms = pipeAnalyzer.GetAllConnected(pipeAnalyzer.Programs[0]);
            Assert.That(connectedPrograms.Count, Is.EqualTo(6));
        }

        [Test]
        public void Can_solve_2017_day_12_part_1()
        {
            var input = PipeAnalyzerPuzzleInput.GetPuzzleInput();
            var pipeAnalyzer = new PipeAnalyzer(input);
            var connectedPrograms = pipeAnalyzer.GetAllConnected(pipeAnalyzer.Programs[0]);
            Assert.That(connectedPrograms.Count, Is.EqualTo(141));
        }

        [Test]
        public void Can_get_number_of_Groups()
        {
            var input = GetExampleInput();
            var pipeAnalyzer = new PipeAnalyzer(input);
            var groups = pipeAnalyzer.GetAllGroups();
            Assert.That(groups.Count, Is.EqualTo(2));
        }

        [Test]
        public void Can_solve_2017_day_12_part_2()
        {
            var input = PipeAnalyzerPuzzleInput.GetPuzzleInput();
            var pipeAnalyzer = new PipeAnalyzer(input);
            var groups = pipeAnalyzer.GetAllGroups();
            Assert.That(groups.Count, Is.EqualTo(171));
        }

        public List<string> GetExampleInput()
        {
            return new List<string>
            {
                "0 <-> 2",
                "1 <-> 1",
                "2 <-> 0, 3, 4",
                "3 <-> 2, 4",
                "4 <-> 2, 3, 6",
                "5 <-> 6",
                "6 <-> 4, 5"
            };
        }
    }
}
