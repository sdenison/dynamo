using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.AdventOfCode;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.AdventOfCode
{
    [TestFixture]
    public class BoxScannerTests
    {
        [Test]
        public void Can_scan_label_with_no_repeating_letters()
        {
            var boxScanner = new BoxScanner();
            var input = "abcdef";
            var labelType = boxScanner.Scan(input);
            Assert.AreEqual(BoxLabelType.None, labelType);
        }

        [Test]
        public void Can_scan_label_with_both_letters()
        {
            var boxScanner = new BoxScanner();
            var input = "bababc";
            var labelType = boxScanner.Scan(input);
            Assert.AreEqual(BoxLabelType.Both, labelType);
        }

        [TestCase("abbcde")]
        [TestCase("aabcdd")]
        public void Can_scan_label_with_match_two(string input)
        {
            var boxScanner = new BoxScanner();
            var labelType = boxScanner.Scan(input);
            Assert.AreEqual(BoxLabelType.MatchTwo, labelType);
        }

        [TestCase("abcccd")]
        [TestCase("ababab")]
        public void Can_scan_label_with_match_three(string input)
        {
            var boxScanner = new BoxScanner();
            var labelType = boxScanner.Scan(input);
            Assert.AreEqual(BoxLabelType.MatchThree, labelType);
        }
    }
}
