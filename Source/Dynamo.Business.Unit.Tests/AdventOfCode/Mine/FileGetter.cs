using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Business.Unit.Tests.AdventOfCode.Mine
{
    public static class FileGetter
    {
        //Gets a MemoryStream from a file.
        public static MemoryStream GetMemoryStreamFromFile(string fileName)
        {
            var resourcePath = @"Dynamo.Business.Unit.Tests." + fileName;
            var assembly = Assembly.GetExecutingAssembly();
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            if (fileStream == null)
                throw new Exception("Could not open file for test");
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return memoryStream;
        }
    }
}
