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
        private int? _multiple;
           
        public Generator(string id, long multiplicationFactor, long initialValue)
        {
            Id = id;
            MultiplicationFactor = multiplicationFactor;
            _lastNumberGeneratred = initialValue;
            _multiple = new int?();
        }

        public Generator(string id, long multiplicationFactor, long initialValue, int multiple)
        {
            Id = id;
            MultiplicationFactor = multiplicationFactor;
            _lastNumberGeneratred = initialValue;
            _multiple = multiple;
        }

        public long NextNumber()
        {
            if (!_multiple.HasValue)
            {
                var nextNumber = (_lastNumberGeneratred * MultiplicationFactor) % 2147483647;
                _lastNumberGeneratred = nextNumber;
                return nextNumber;
            }
            else
            {
                while(true)
                {
                    var nextNumber = (_lastNumberGeneratred * MultiplicationFactor) % 2147483647;
                    _lastNumberGeneratred = nextNumber;
                    if (nextNumber % _multiple == 0)
                        return nextNumber;
                }
            }
        }

        public long NextNumber(int multiple)
        {
            while(true)
            {
                var nextNumber = NextNumber();
                if (nextNumber % multiple == 0)
                    return nextNumber;
            }
        }
    }
}
