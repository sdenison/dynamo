using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Programs.Plumber
{
    public class PipeAnalyzer
    {
        public List<Program> Programs { get; set; }

        public PipeAnalyzer(List<string> inputStrings)
        {
            Programs = new List<Program>();
            foreach (var inputString in inputStrings)
            {
                var pipeId = int.Parse(inputString.Split(' ')[0]);
                Programs.Add(new Program(pipeId));

            }
            foreach (var inputString in inputStrings)
            {
                var linkedPrograms = inputString.Split("<->")[1];
                var programId = int.Parse(inputString.Split(' ')[0]);
                var currentProgram = Programs.Single(x => x.ProgramId == programId);
                foreach (var linkedPipe in linkedPrograms.Split(","))
                {
                    var neighborProgram = Programs.Single(x => x.ProgramId == int.Parse(linkedPipe));
                    currentProgram.AddNeighbor(neighborProgram);
                }
            }
        }

        public List<Program> GetAllConnected(Program program)
        {
            var connectdPrograms = new List<Program>();
            program.FindAllNeighbors(connectdPrograms);
            return connectdPrograms;
        }

        public List<List<Program>> GetAllGroups()
        {
            var groups = new List<List<Program>>();
            foreach (var program in Programs)
            {
                var group = GetAllConnected(program).OrderBy(x => x.ProgramId).ToList();
                var groupExists = false;
                foreach (var existingGroup in groups)
                {
                    if (GroupsAreEqual(existingGroup, group))
                    {
                        groupExists = true;
                        break;
                    }
                }
                if (!groupExists)
                {
                    groups.Add(group);
                }
            }
            return groups;
        }

        public bool GroupsAreEqual(List<Program> program1, List<Program> program2)
        {
            if (program1.Count != program2.Count)
                return false;
            for (var i = 0; i < program1.Count; i++)
            {
                if (program1[i].ProgramId != program2[i].ProgramId)
                    return false;
            }
            return true;
        }

    }
}
