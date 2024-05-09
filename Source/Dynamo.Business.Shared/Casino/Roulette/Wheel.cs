using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Wheel
    {
        public List<Space> Spaces { get; set; }
        public Space WinningSpace { get; private set; }

        public Wheel()
        {
            Spaces = new List<Space>();
            Spaces.Add(new Space() { Value = SpaceType.AllForOne });
            Spaces.Add(new Space() { Value = SpaceType.One });
            Spaces.Add(new Space() { Value = SpaceType.Two });
            Spaces.Add(new Space() { Value = SpaceType.Three });
            Spaces.Add(new Space() { Value = SpaceType.Four });
            Spaces.Add(new Space() { Value = SpaceType.Five });
            Spaces.Add(new Space() { Value = SpaceType.Six });
            Spaces.Add(new Space() { Value = SpaceType.Seven });
            Spaces.Add(new Space() { Value = SpaceType.Eight });
            Spaces.Add(new Space() { Value = SpaceType.Nine });
            Spaces.Add(new Space() { Value = SpaceType.Ten });
        }

        public Space Spin(SpaceType startingSpace, double wheelSpeed)
        {
            var startingSpaceIndex = Spaces.IndexOf(Spaces.First(x => x.Value == startingSpace));
            var winningSpaceIndex = (int)(startingSpaceIndex + wheelSpeed) % Spaces.Count;
            return Spaces[winningSpaceIndex];
        }
    }
}
