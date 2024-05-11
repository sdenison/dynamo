using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Wheel
    {
        public List<Space> Spaces { get; set; }

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

        public Space Spin(SpaceType startingSpace, int wheelSpeed)
        {
            var startingSpaceIndex = Spaces.IndexOf(Spaces.First(x => x.Value == startingSpace));
            var winningSpaceIndex = (startingSpaceIndex + wheelSpeed) % Spaces.Count;
            return Spaces[winningSpaceIndex];
        }

        public static int SpinWheelComplex(int initialBallPosition, float decelerationCoef)
        {
            var random = new Random();
            // Generate initial wheel speed with normal distribution
            double initialWheelSpeed = Normal.WithMeanStdDev(0.5, 0.1).Sample();
            // Generate ball's initial speed with normal distribution
            double ballSpeed = Normal.WithMeanStdDev(1.5, 0.1).Sample();

            double wheelPosition = 0;
            double ballPosition = initialBallPosition;

            while (ballSpeed > 0.1 || initialWheelSpeed > 0.1)
            {
                ballPosition = (ballPosition + ballSpeed) % 11;
                wheelPosition = (wheelPosition + initialWheelSpeed) % 11;

                // Deceleration
                ballSpeed = Math.Max(0, ballSpeed - decelerationCoef);
                initialWheelSpeed = Math.Max(0, initialWheelSpeed - 0.01);

                // Chance of bouncing
                if (random.NextDouble() < 0.1)
                {
                    int[] bounceDistances = { -1, 1, -2, 2 };
                    int bounceDistance = bounceDistances[random.Next(bounceDistances.Length)];
                    ballPosition = (ballPosition + bounceDistance) % 11;
                    // Reduce speed slightly after bouncing
                    ballSpeed *= 0.9;
                }

                // Simulate the chance of the ball getting caught in a slot
                if (random.NextDouble() < 0.05 * (1 - ballSpeed))
                {
                    // 95% chance to get caught when slow enough
                    bool caught = random.NextDouble() < 0.95;
                    if (caught)
                    {
                        return (int)ballPosition;
                    }
                }
            }

            // Ball settles in the nearest slot if not caught earlier
            int finalPosition = (int)Math.Round(ballPosition) % 11;

            return finalPosition;
        }
    }
}
