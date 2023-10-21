using System;
using System.Collections.Generic;
using System.IO;

namespace Dynamo.Business.Shared.Utilities
{
    public static class FileReader
    {
        public static string[] ReadFileContents(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            List<string> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
            return lines.ToArray();
        }
    }
}
