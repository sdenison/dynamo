using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Programs
{
    public class Program
    {
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public List<Program> Children { get; private set; }
        public bool HasParent { get; private set; }

        public Program(string name, int weight)
        {
            Name = name;
            Weight = weight;
            Children = new List<Program>();
            HasParent = false;
        }

        public static Program Parse(string programString)
        {
            var name = programString.Split(' ')[0];
            var weight = programString.Split(" ")[1].Replace("(", "").Replace(")", "");
            return new Program(name, int.Parse(weight));
        }

        public static Program Parse(List<string> programStrings)
        {
            var programs = new Dictionary<string, Program>();
            // Load all programs
            foreach (var programString in programStrings)
            {
                var program = Program.Parse(programString);
                programs.Add(program.Name, program);
            }
            // Handle programs with children
            foreach (var programString in programStrings.Where(x => x.Contains("->")))
            {
                var programName = programString.Split(" ")[0].Trim();
                foreach (var childProgramName in programString.Split("->")[1].Split(","))
                {
                    programs[programName].AddChild(programs[childProgramName.Trim()]);
                }
            }
            // Return the program with no parents
            return programs.Values.Single(x => !x.HasParent);
        }

        public void AddChild(Program child)
        {
            child.HasParent = true;
            Children.Add(child);
        }

        public Program GetLastChild()
        {
            if (Children.Any())
                return Children[Children.Count - 1].GetLastChild();
            else
                return this;
        }

        public int GetTotalWeight()
        {
            var totalWeight = this.Weight;
            foreach (var child in Children)
            {
                totalWeight += child.GetTotalWeight();
            }
            return totalWeight;
        }

        public int GetUnbalancedWeight(int diff = 0)
        {
            var weights = new Dictionary<int, int>();
            foreach (var child in Children)
            {
                if (weights.ContainsKey(child.GetTotalWeight()))
                {
                    weights[child.GetTotalWeight()]++;
                }
                else
                {
                    weights.Add(child.GetTotalWeight(), 1);
                }
            }
            if (weights.Count > 1)
            {
                var oddManOut = weights.Single(x => x.Value == 1).Key;
                var matchingWeights = weights.Single(x => x.Value > 1).Key;
                return Children.Single(x => x.GetTotalWeight() == oddManOut).GetUnbalancedWeight(oddManOut - matchingWeights);
            }
            if (weights.Count == 1)
            {
                return Weight - diff;
            }
            return 0;

        }
    }
}
