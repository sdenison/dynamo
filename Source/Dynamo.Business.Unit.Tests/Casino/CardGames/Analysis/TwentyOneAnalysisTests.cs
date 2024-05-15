using Dynamo.Business.Shared.Casino.CardGames.Analysis;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Unit.Tests.Casino.CardGames.Analysis
{
    public class TwentyOneAnalysisTests
    {
        [Test]
        public void Can_analyze_twenty_one_games()
        {
            var twentyOneAnalysis = TwentyOneAnalysis.Parse(GetFileInput());
            Assert.That(twentyOneAnalysis.PlayOutcomes.Count(), Is.EqualTo(900000));
        }

        private string[] GetFileInput()
        {
            var stream = Tests.FileGetter.GetMemoryStreamFromFile("blkjckhands.csv");
            stream.Seek(0, SeekOrigin.Begin);
            stream.Position = 0;
            string[] stringArray = ReadMemoryStreamToStringArray(stream, Encoding.UTF8);
            return stringArray;
        }

        static string[] ReadMemoryStreamToStringArray(MemoryStream memoryStream, Encoding encoding)
        {
            // Reset the memoryStream position to the beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read the data from the memory stream into a byte array
            byte[] buffer = new byte[memoryStream.Length];
            memoryStream.Read(buffer, 0, buffer.Length);

            // Convert the byte array to a string using the specified encoding
            string content = encoding.GetString(buffer);

            // Split the content into an array of strings based on a delimiter, e.g., newline
            string[] stringArray = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return stringArray;
        }

    }
}
