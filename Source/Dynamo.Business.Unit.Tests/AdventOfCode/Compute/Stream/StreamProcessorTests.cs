using Dynamo.Business.Shared.AdventOfCode.Compute.Stream;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Stream
{
    [TestFixture]
    public class StreamProcessorTests
    {
        [Test]
        public void Can_count_groups_1()
        {
            var stream = "{}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(1));
        }

        [Test]
        public void Can_count_groups_2()
        {
            var stream = "{{{}}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(3));
        }

        [Test]
        public void Can_count_groups_3()
        {
            var stream = "{{},{}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(3));
        }

        [Test]
        public void Can_count_groups_4()
        {
            var stream = "{{{},{},{{}}}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(6));
        }

        [Test]
        public void Can_count_groups_5()
        {
            var stream = "{<{},{},{{}}>}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(1));
        }

        [Test]
        public void Can_count_groups_6()
        {
            var stream = "{<a>,<a>,<a>,<a>}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(1));
        }

        [Test]
        public void Can_count_groups_7()
        {
            var stream = "{{<a>},{<a>},{<a>},{<a>}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(5));
        }

        [Test]
        public void Can_count_groups_8()
        {
            var stream = "{{<!>},{<!>},{<!>},{<a>}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNumberOfGroups(), Is.EqualTo(2));
        }

        [Test]
        public void Can_get_group_score_1()
        {
            var stream = "{}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetGroupScore(), Is.EqualTo(1));
        }

        [Test]
        public void Can_get_group_score_2()
        {
            var stream = "{{{}}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetGroupScore(), Is.EqualTo(6));
        }

        [Test]
        public void Can_get_group_score_3()
        {
            var stream = "{{<a!>},{<a!>},{<a!>},{<ab>}}";
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetGroupScore(), Is.EqualTo(3));
        }

        [Test]
        public void Can_solve_day_9_part_1()
        {
            var stream = PuzzleInputFactory.GetPuzzleInput();
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetGroupScore(), Is.EqualTo(12505));
        }

        [Test]
        public void Can_solve_day_9_part_2()
        {
            var stream = PuzzleInputFactory.GetPuzzleInput();
            var streamProcessor = new StreamProcessor(stream);
            Assert.That(streamProcessor.GetNoncanceledCharacterCount(), Is.EqualTo(6671));
        }
    }
}
