using System;
using System.IO;
using System.Reflection;

namespace Dynamo.Business.Unit.Tests.Utilities
{
    internal static class FileGetter
    {
        //Gets a MemoryStream from a file.
        public static MemoryStream GetMemoryStreamFromFile(string fileName)
        {
            var resourcePath = @"Dynamo.Business.Unit.Tests.FakeFiles.Utilities." + fileName;
            var assembly = Assembly.GetExecutingAssembly();
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            if (fileStream == null)
                throw new Exception("Could not open example excel file for test");
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return memoryStream;
        }
    }
}
