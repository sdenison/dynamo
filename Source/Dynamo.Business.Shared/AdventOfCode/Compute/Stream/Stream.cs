namespace Dynamo.Business.Shared.AdventOfCode.Compute.Stream
{
    public class Stream
    {
        public char[] StreamChars { get; private set; }
        public int Index { get; set; }

        public Stream(string stream)
        {
            StreamChars = stream.ToCharArray();
            Index = 0;
        }

        public bool HasNextValue()
        {
            return Index < StreamChars.Length;
        }

        public char GetNextChar()
        {
            var nextChar = StreamChars[Index];
            Index++;
            return nextChar;
        }
    }
}
