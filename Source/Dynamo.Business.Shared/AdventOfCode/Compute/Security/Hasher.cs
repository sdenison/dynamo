namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Hasher
    {
        public int[] Elements { get; private set; }
        public int CurrentPosition { get; private set; }
        public int SkipSize { get; private set; }

        public Hasher(int size)
        {
            CurrentPosition = 0;
            SkipSize = 0;
            Elements = new int[size];
            for (var i = 0; i < size; i++)
            {
                Elements[i] = i;
            }
        }

        public int GetElement(int index)
        {
            return Elements[index % Elements.Length];
        }

        public void ApplyLength(int length)
        {
            for (var i = 0; i < length / 2; i++)
            {
                var element1 = Elements[i + CurrentPosition];
                var element2 = Elements[CurrentPosition + ((length - 1) - i)];
                Elements[i + CurrentPosition] = element2;
                Elements[CurrentPosition + ((length - 1) - i)] = element1;
            }
            CurrentPosition += SkipSize + length;
        }
    }
}
