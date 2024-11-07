using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Cpu
{
    public class Instruction
    {
        public string OperationRegister { get; private set; }
        public OperationType Operation { get; private set; }
        public int Amount { get; private set; }
        public string ConditionRegister { get; private set; }
        public ConditionOperator ConditionOperator { get; private set; }
        public int ConditionValue { get; private set; }
        public bool IsValid { get; private set; }
        public string ValidationMessage { get; private set; }
        public bool IsCurrent { get; set; } = false;
        public bool IsBreakpoint { get; set; } = false;
        public bool BreakpointHasBeenHit { get; set; } = false;
        public string InstructionString { get; set; }


        public Instruction(string instructionString)
        {
            InstructionString = instructionString;
            OperationRegister = instructionString.Split(" ")[0];
            Operation = (OperationType)Enum.Parse(typeof(OperationType), instructionString.Split(" ")[1], ignoreCase: true);
            Amount = int.Parse(instructionString.Split(" ")[2]);
            if (!instructionString.Split(" ")[3].ToLower().Equals("if"))
                throw new ArgumentException($"The instruction string shoulud include \"if\". instructionString = {instructionString}");
            ConditionRegister = instructionString.Split(" ")[4];
            ConditionOperator = ParseOperator(instructionString.Split(" ")[5]);
            ConditionValue = int.Parse(instructionString.Split(" ")[6]);
        }

        public static ConditionOperator ParseOperator(string symbol)
        {
            return symbol switch
            {
                ">" => ConditionOperator.GreaterThan,
                ">=" => ConditionOperator.GreaterThanOrEqual,
                "<" => ConditionOperator.LessThan,
                "<=" => ConditionOperator.LessThanOrEqual,
                "==" => ConditionOperator.Equal,
                "!=" => ConditionOperator.NotEqual,
                _ => throw new ArgumentException($"Invalid comparison operator: {symbol}")
            };
        }
    }

    public enum OperationType
    {
        Inc,
        Dec
    }

    public enum ConditionOperator
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Equal,
        NotEqual
    }
}
