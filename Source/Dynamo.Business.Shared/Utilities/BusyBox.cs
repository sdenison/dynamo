using System.IO;
using System.Linq.Expressions;
using System.Threading;

namespace Dynamo.Business.Shared.Utilities
{
    //This is just a stand in to test what would happen if we had an actual workload
    public static class BusyBox
    {
        public static void Sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static int GetSecondFromStream(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            int secondFromStream = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                secondFromStream = int.Parse(line);
            }

            return secondFromStream;
        }
    }
}
