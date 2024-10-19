using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Cpu
{
    public class Processor
    {
        public Dictionary<string, int> Registers { get; private set; }
        public int HighestRegisterValueSeen { get; private set; }
        public List<Instruction> Instructions { get; private set; }
        public int InstructionPointer { get; set; }
        public bool ProgramIsRunning { get; set; } = false;

        public Processor(string[] instructions)
        {
            Registers = new Dictionary<string, int>();
            Instructions = new List<Instruction>();
            foreach (var instruction in instructions)
            {
                var newInstruction = new Instruction(instruction);
                Instructions.Add(newInstruction);
                if (!Registers.ContainsKey(newInstruction.OperationRegister))
                {
                    Registers.Add(newInstruction.OperationRegister, 0);
                }
                if (!Registers.ContainsKey(newInstruction.ConditionRegister))
                {
                    Registers.Add(newInstruction.ConditionRegister, 0);
                }
            }
            Instructions[0].IsCurrent = true;
            InstructionPointer = 0;
        }

        public void RunNextInstruction()
        {
            ProgramIsRunning = true;
            var instruction = Instructions[InstructionPointer];
            var operationRegister = Registers[instruction.OperationRegister];
            var conditionRegister = Registers[instruction.ConditionRegister];
            var condition = false;
            switch (instruction.ConditionOperator)
            {
                case ConditionOperator.Equal:
                    if (conditionRegister == instruction.ConditionValue)
                        condition = true;
                    break;
                case ConditionOperator.NotEqual:
                    if (conditionRegister != instruction.ConditionValue)
                        condition = true;
                    break;
                case ConditionOperator.GreaterThan:
                    if (conditionRegister > instruction.ConditionValue)
                        condition = true;
                    break;
                case ConditionOperator.GreaterThanOrEqual:
                    if (conditionRegister >= instruction.ConditionValue)
                        condition = true;
                    break;
                case ConditionOperator.LessThan:
                    if (conditionRegister < instruction.ConditionValue)
                        condition = true;
                    break;
                case ConditionOperator.LessThanOrEqual:
                    if (conditionRegister <= instruction.ConditionValue)
                        condition = true;
                    break;
            }
            if (condition)
            {
                if (instruction.Operation == OperationType.Inc)
                    Registers[instruction.OperationRegister] += instruction.Amount;
                if (instruction.Operation == OperationType.Dec)
                    Registers[instruction.OperationRegister] -= instruction.Amount;
                if (Registers[instruction.OperationRegister] > HighestRegisterValueSeen)
                    HighestRegisterValueSeen = Registers[instruction.OperationRegister];
            }
            InstructionPointer++;
            if (InstructionPointer == Instructions.Count)
            {
                InstructionPointer = 0;
                ProgramIsRunning = false;
            }
        }

        public void RunInstructions()
        {
            HighestRegisterValueSeen = 0;
            ProgramIsRunning = true;

            while (ProgramIsRunning)
            {
                var currentInstruction = Instructions[InstructionPointer];
                if (currentInstruction.IsBreakpoint && !currentInstruction.BreakpointHasBeenHit)
                {
                    currentInstruction.BreakpointHasBeenHit = true;
                    break;
                }
                RunNextInstruction();
            }
        }

        public void ResetRegisters()
        {
            foreach (var registerKeys in Registers.Keys)
            {
                Registers[registerKeys] = 0;
            }
            ProgramIsRunning = false;
        }
    }
}
