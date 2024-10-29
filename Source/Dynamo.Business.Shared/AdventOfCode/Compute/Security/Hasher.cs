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

        public void SetElement(int index, int value)
        {
            Elements[index % Elements.Length] = value;
        }

        public void ApplyLength(int length)
        {
            for (var i = 0; i < length / 2; i++)
            {
                var element1 = GetElement(i + CurrentPosition);
                var element2 = GetElement(CurrentPosition + ((length - 1) - i));
                SetElement(i + CurrentPosition, element2);
                SetElement(CurrentPosition + ((length - 1) - i), element1);
            }
            CurrentPosition = (CurrentPosition + SkipSize + length) % Elements.Length;
            SkipSize++;
        }

        public int GetHash(int[] salt)
        {
            foreach (var length in salt)
            {
                ApplyLength(length);
            }
            return Elements[0] * Elements[1];
        }
    }
}
