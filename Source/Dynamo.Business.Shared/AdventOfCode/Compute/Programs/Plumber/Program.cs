using System.Collections.Generic;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Programs.Plumber
{
    public class Program
    {
        public int ProgramId { get; set; }
        public List<Program> Neighbors { get; set; }

        public Program(int programId)
        {
            Neighbors = new List<Program>();
            ProgramId = programId;
        }

        public void AddNeighbor(Program neighbor)
        {
            Neighbors.Add(neighbor);
        }

        public List<Program> FindAllNeighbors(List<Program> connectedPrograms)
        {
            if (!connectedPrograms.Contains(this))
            {
                connectedPrograms.Add(this);
                foreach (var program in this.Neighbors)
                {
                    if (!connectedPrograms.Contains(program))
                        program.FindAllNeighbors(connectedPrograms);
                }
            }
            return connectedPrograms;
        }
    }
}
