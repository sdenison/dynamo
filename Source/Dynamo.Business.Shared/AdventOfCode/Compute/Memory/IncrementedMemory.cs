namespace Dynamo.Business.Shared.AdventOfCode.Compute.Memory
{
    public class IncrementedMemory : SpiralMemory
    {
        protected override bool ShouldExit(int value, int limit) => value == limit;
    }
}
