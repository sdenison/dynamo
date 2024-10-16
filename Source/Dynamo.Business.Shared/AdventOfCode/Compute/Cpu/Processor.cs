using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Cpu
{
    public class Processor
    {
        public Dictionary<string, int> Registers { get; set; }
        public List<Instruction> Instructions { get; private set; }

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
        }

        public void RunInstructions()
        {
            foreach (var instruction in Instructions)
            {
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
                }
            }
        }
    }
}
