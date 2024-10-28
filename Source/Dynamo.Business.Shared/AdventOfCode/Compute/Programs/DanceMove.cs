using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Programs
{
    public class DanceMove
    {
        public Operation Operation { get; set; }
        public int? Spin { get; set; }
        public int? Program1Index { get; set; }
        public int? Program2Index { get; set; }
        public string Program1Name { get; set; }
        public string Program2Name { get; set; }

        public DanceMove(string danceMove)
        {
            if (danceMove[0] == 's')
            {
                Operation = Operation.Spin;
                danceMove = danceMove.Substring(1);
                Spin = int.Parse(danceMove);
            }
            if (danceMove[0] == 'x')
            {
                Operation = Operation.SwapByPositions;
                danceMove = danceMove.Substring(1);
                Program1Index = int.Parse(danceMove.Split('/')[0]);
                Program2Index = int.Parse(danceMove.Split('/')[1]);
            }
            if (danceMove[0] == 'p')
            {
                Operation = Operation.SwapByProgramName;
                danceMove = danceMove.Substring(1);
                Program1Name = danceMove.Split('/')[0];
                Program2Name = danceMove.Split('/')[1];
            }
        }
    }
}
