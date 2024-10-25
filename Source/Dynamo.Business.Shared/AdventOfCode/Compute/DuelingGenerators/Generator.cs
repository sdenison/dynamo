using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.DuelingGenerators
{
    public class Generator
    {
        public string Id { get; private set; }
        public long MultiplicationFactor { get; private set; }
        private long _lastNumberGeneratred = 0;
           
        public Generator(string id, long multiplicationFactor, long initialValue)
        {
            Id = id;
            MultiplicationFactor = multiplicationFactor;
            _lastNumberGeneratred = initialValue;
        }

        public long NextNumber()
        {
            var nextNumber = (_lastNumberGeneratred * MultiplicationFactor) % 2147483647;
            _lastNumberGeneratred = nextNumber;
            return nextNumber;
        }
    }
}
