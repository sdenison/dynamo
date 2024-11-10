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

        //public int GetNumberOfPipesInGroup(int pipeId)
        //{
        //    var numberOfPipesInGroup = 0;
        //    foreach (var pipe in Pipes)
        //    {
        //        if (pipe.Key == pipeId)
        //        {
        //            numberOfPipesInGroup++;
        //        }
        //        else if (pipe.Value.Contains(pipeId))
        //        {
        //            numberOfPipesInGroup++;
        //        }
        //        else
        //        {
        //            foreach (var subPipe in Pipes)
        //            {
        //            }
        //        }
        //    }
        //}
    }
}
