using System;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Grids
{
    public class Coordinate
    {
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }

        public Coordinate()
        {
            Q = 0;
            R = 0;
            S = 0;
        }

        public bool StepTowardZero()
        {
            if (Q == 0 && R == 0 && S == 0)
                return false;
            else if (Q == 0)
            {
                R = MoveTowardZero(R);
                S = MoveTowardZero(S);
                return true;
            }
            else if (R == 0)
            {
                Q = MoveTowardZero(Q);
                S = MoveTowardZero(S);
                return true;
            }
            else if (S == 0)
            {
                Q = MoveTowardZero(Q);
                R = MoveTowardZero(R);
                return true;
            }
            if (Math.Abs(Q) <= Math.Abs(R) && Math.Abs(Q) <= Math.Abs(S))
            {
                if (Q < 0)
                    R = MoveTowardZero(R);
                else
                    S = MoveTowardZero(S);
                Q = MoveTowardZero(Q);
            }
            else if (Math.Abs(R) <= Math.Abs(Q) && Math.Abs(R) <= Math.Abs(S))
            {
                if (R < 0)
                    Q = MoveTowardZero(Q);
                else
                    S = MoveTowardZero(S);
                R = MoveTowardZero(R);
            }
            else if (Math.Abs(S) <= Math.Abs(Q) && Math.Abs(S) <= Math.Abs(R))
            {
                if (S < 0)
                    R = MoveTowardZero(R);
                else
                    Q = MoveTowardZero(Q);
                S = MoveTowardZero(S);
            }
            return true;
        }

        private int MoveTowardZero(int value)
        {
            if (value > 0)
                return value - 1;
            else if (value < 1)
                return value + 1;
            else
                return value;
        }

        public void Add(Coordinate coord)
        {
            Q += coord.Q;
            R += coord.R;
            S += coord.S;
        }

        public Coordinate(string direction)
        {
            switch (direction)
            {
                case "ne":
                    Q = 1; R = -1; S = 0;
                    break;
                case "n":
                    Q = 0; R = -1; S = 1;
                    break;
                case "nw":
                    Q = -1; R = 0; S = 1;
                    break;
                case "sw":
                    Q = -1; R = 1; S = 0;
                    break;
                case "s":
                    Q = 0; R = 1; S = -1;
                    break;
                case "se":
                    Q = 1; R = 0; S = -1;
                    break;
                default:
                    throw new ArgumentException($"Unknown direction {direction}");
            };
        }
    }
}
