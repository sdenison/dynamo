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
        public void Find_answer_spring_2024_week_6_part_1()
        {
            var twentyOneAnalysis = TwentyOneAnalysis.Parse(GetFileInput());
            Assert.That(twentyOneAnalysis.PlayOutcomes.Count(), Is.EqualTo(900000));

            //Did not work
            var playerAndDealerBust = twentyOneAnalysis.PlayOutcomes.Where(x => x.PlayerBustBeat == PlayerBustBeat.Bust && x.DealerBustBeat == DealerBustBeat.Bust).Count();
            var percentage = (double)playerAndDealerBust / twentyOneAnalysis.PlayOutcomes.Count;
            Assert.That(percentage, Is.EqualTo(0));

            //Did not work
            playerAndDealerBust = twentyOneAnalysis.PlayOutcomes.Where(x => x.PlayerBustBeat == PlayerBustBeat.DlBust && x.DealerBustBeat == DealerBustBeat.PlBust).Count();
            percentage = (double)playerAndDealerBust / twentyOneAnalysis.PlayOutcomes.Count;
            Assert.That(percentage, Is.EqualTo(0));

            playerAndDealerBust = twentyOneAnalysis.PlayOutcomes.Where(x => x.SumOfCards > 21 && x.SumOfDeal > 21).Count();
            percentage = (double)playerAndDealerBust / twentyOneAnalysis.PlayOutcomes.Count;
            //Accepted answer was 0.039
            Assert.That(Math.Round(percentage * 1000), Is.EqualTo(39));
        }

        [Test, Ignore("Takes too long to run in NUnit")]
        public void Find_answer_spring_2024_week_6_part_2()
        {
            var twentyOneAnalysis = TwentyOneAnalysis.Parse(GetFileInput());
            var bootstrapProportions = new List<double>();
            var bootstrapSamples = 1000;

            for (int i = 0; i < bootstrapSamples; i++)
            {
                var resampledOutcomes = twentyOneAnalysis.Resample();
                var playerAndDealerBust = resampledOutcomes.Count(x => x.SumOfCards > 21 && x.SumOfDeal > 21);
                var proportion = (double)playerAndDealerBust / twentyOneAnalysis.PlayOutcomes.Count;
                bootstrapProportions.Add(proportion);
            }

            bootstrapProportions.Sort();
            var lowerBound = Percentile(bootstrapProportions, 2.5);
            var upperBound = Percentile(bootstrapProportions, 97.5);

            //Accepted answers were 0.38 for the lower bound and 0.39 for the upper bound
            Assert.That(Math.Round(lowerBound * 1000), Is.EqualTo(38));
            Assert.That(Math.Round(upperBound * 1000), Is.EqualTo(39));
        }

        private double Percentile(List<double> values, double percentile)
        {
            int n = (int)((percentile / 100.0) * values.Count);
            return values[Math.Max(0, n - 1)];
        }

        private string[] GetFileInput()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("blkjckhands.csv");
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
