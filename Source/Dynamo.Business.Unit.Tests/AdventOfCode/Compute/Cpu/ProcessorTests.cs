using Dynamo.Business.Shared.AdventOfCode.Compute.Cpu;
using NUnit.Framework;
using System.Linq;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Compute.Cpu
{
    [TestFixture]
    public class ProcessorTests
    {

        [Test]
        public void Can_parse_instruction()
        {
            var instruction = new Instruction("b inc 5 if a > 1");
            Assert.That(instruction.OperationRegister, Is.EqualTo("b"));
            Assert.That(instruction.Operation, Is.EqualTo(OperationType.Inc));
            Assert.That(instruction.Amount, Is.EqualTo(5));
            Assert.That(instruction.ConditionRegister, Is.EqualTo("a"));
            Assert.That(instruction.ConditionOperator, Is.EqualTo(ConditionOperator.GreaterThan));
            Assert.That(instruction.ConditionValue, Is.EqualTo(1));
        }

        [Test]
        public void Can_create_processor()
        {
            string[] instructions =
            {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            };

            var processor = new Processor(instructions);
            processor.RunInstructions();
            Assert.That(processor.Registers["b"], Is.EqualTo(0));
            Assert.That(processor.Registers["a"], Is.EqualTo(1));
            Assert.That(processor.Registers["c"], Is.EqualTo(-10));
        }

        [Test]
        public void Can_reset_registers()
        {
            string[] instructions =
            {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            };

            var processor = new Processor(instructions);
            processor.RunInstructions();
            Assert.That(processor.Registers["b"], Is.EqualTo(0));
            Assert.That(processor.Registers["a"], Is.EqualTo(1));
            Assert.That(processor.Registers["c"], Is.EqualTo(-10));
            processor.ResetRegisters();
            Assert.That(processor.Registers["b"], Is.EqualTo(0));
            Assert.That(processor.Registers["a"], Is.EqualTo(0));
            Assert.That(processor.Registers["c"], Is.EqualTo(0));
        }

        [Test]
        public void Can_solve_2017_day_8_part_1()
        {
            string[] instructions = PuzzleInputFactory.GetPuzzleInput();
            var processor = new Processor(instructions);
            processor.RunInstructions();
            var maxRegisterValue = processor.Registers.Values.Max();
            Assert.That(maxRegisterValue, Is.EqualTo(5215));
        }


        [Test]
        public void Can_solve_2017_day_8_part_2()
        {
            string[] instructions = PuzzleInputFactory.GetPuzzleInput();
            var processor = new Processor(instructions);
            processor.RunInstructions();
            Assert.That(processor.HighestRegisterValueSeen, Is.EqualTo(6419));
        }
    }
}
