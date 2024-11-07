using Dynamo.Business.Shared.AdventOfCode.Compute.Programs;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Programs
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Can_create_Program()
        {
            var program = Program.Parse("pbga (66)");
            Assert.That(program.Name, Is.EqualTo("pbga"));
            Assert.That(program.Weight, Is.EqualTo(66));
        }

        [Test]
        public void Can_create_Program_data_structure_from_list_of_string()
        {
            var programStrings = new List<string>()
            {
                "pbga (66)",
                "xhth (57)",
                "ebii (61)",
                "havc (66)",
                "ktlj (57)",
                "fwft (72) -> ktlj, cntj, xhth",
                "qoyq (66)",
                "padx (45) -> pbga, havc, qoyq",
                "tknk (41) -> ugml, padx, fwft",
                "jptl (61)",
                "ugml (68) -> gyxo, ebii, jptl",
                "gyxo (61)",
                "cntj (57)"
            };
            var program = Program.Parse(programStrings);
            Assert.That(program.Name, Is.EqualTo("tknk"));
            var lastChild = program.GetLastChild();
            Assert.That(lastChild.Name, Is.EqualTo("xhth"));
        }
    }
}
