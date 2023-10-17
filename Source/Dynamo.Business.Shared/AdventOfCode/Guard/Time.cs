using Dynamo.Business.Shared.AdventOfCode.Cloth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Guard
{
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public override string ToString()
        {
            return $"Hour: {Hour}, Minute: {Minute}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Time other = (Time)obj;
            return Hour == other.Hour && Minute == other.Minute;
        }
    }
}
