namespace Dynamo.Business.Shared.AdventOfCode.Compute.Stream
{
    public class StreamProcessor
    {
        public Group Group { get; set; }

        public StreamProcessor(string stream)
        {
            Group = new Group(1);
            ProcessStream(stream);
        }

        public void ProcessStream(string streamString)
        {
            var stream = new Stream(streamString);
            ProcessStream(stream);
        }

        public void ProcessStream(Stream stream)
        {
            stream.Index = 1;
            Group.ProcessStream(stream);
        }

        public int GetNumberOfGroups()
        {
            return Group.GetNumberOfGroups();
        }

        public int GetGroupScore()
        {
            return Group.GetGroupScore();
        }

        public int GetNoncanceledCharacterCount()
        {
            return Group.GetNoncanceledCharacters();
        }
    }
}
