using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Casino.Roulette
{
    public class Wheel
    {
        public Wheel()
        { }

        public SpaceType SpinWheel(SpaceType startingSpace, int wheelSpeed)
        {
            var numberOfSpaces = Enum.GetValues(typeof(SpaceType)).Length;
            return (SpaceType)(((int)startingSpace + wheelSpeed) % numberOfSpaces);
        }

        public static SpaceType SpinWheelComplex(int initialBallPosition, float decelerationCoef)
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
                        return (SpaceType)((int)ballPosition);
                    }
                }
            }

            // Ball settles in the nearest slot if not caught earlier
            int finalPosition = (int)Math.Round(ballPosition) % 11;

            return (SpaceType)finalPosition;
        }
    }
}
