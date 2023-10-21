using Dynamo.Business.Shared.AdventOfCode;
using Dynamo.Business.Shared.AdventOfCode.Warehouse;
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

        [Test]
        public void Can_get_checksum_for_multiple_boxes()
        {
            string[] input = new string[]
            {
                "abcdef",
                "bababc",
                "abbcde",
                "abcccd",
                "aabcdd",
                "abcdee",
                "ababab"
            };
            var boxScanner = new BoxScanner();
            var checkSum = boxScanner.GetCheckSum(input);
            Assert.AreEqual(12, checkSum);
        }

        [Test]
        public void Can_get_offBy1_when_inputs_do_not_come_close()
        {
            var input1 = "asdff";
            var input2 = "fguij";
            var boxScanner = new BoxScanner();
            var offBy1 = boxScanner.OffBy1(input1, input2);
            Assert.IsFalse(offBy1);
        }

        [Test]
        public void Can_get_off_by_number()
        {
            var input1 = "fghij";
            var input2 = "fguij";
            var boxScanner = new BoxScanner();
            var offBy1 = boxScanner.OffBy1(input1, input2);
            Assert.IsTrue(offBy1);
        }

        [Test]
        public void Can_get_matching_part_of_string()
        {
            var input1 = "fghij";
            var input2 = "fguij";
            var boxScanner = new BoxScanner();
            var matching = boxScanner.GetMatchingPart(input1, input2);
            Assert.AreEqual("fgij", matching);
        }

        [Test]
        public void GetMatchingOffByOneLetter_returns_correct_string()
        {
            string[] input = new string[]
            {
                "abcde",
                "fghij",
                "klmno",
                "pqrst",
                "fguij",
                "axcye",
                "wvxyz"
            };
            var boxScanner = new BoxScanner();
            var matching = boxScanner.GetMatchingOffByOneLetter(input);
            Assert.AreEqual("fgij", matching);
        }

        [Test]
        public void Get_day_1_part_1_answer()
        {
            var boxScanner = new BoxScanner();
            var checkSum = boxScanner.GetCheckSum(day1Part1Input);
            Assert.AreEqual(4693, checkSum);
        }

        [Test]
        public void Get_day_1_part_2_answer()
        {
            var boxScanner = new BoxScanner();
            var matching = boxScanner.GetMatchingOffByOneLetter(day1Part1Input);
            Assert.AreEqual("pebjqsalrdnckzfihvtxysomg", matching);
        }

        [Test]
        public void Get_day_1_part_2_answer_from_text_file()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("BoxScans.txt");
        }

        private string[] day1Part1Input = new string[]
        {
            "pnebjqralgdgckzfifvtxywomu",
            "pnebjqsalrdgcqzfihotxhwomu",
            "pneajqsalrdgckzfihytxywoml",
            "pnepjqsalrwgckztihvtxywomu",
            "pnhbjqsalrdgckzfimvtxywodu",
            "pnwbjqsdlrdgckzfihvnxywomu",
            "inebjqnalrdgckzfihvtxzwomu",
            "pnebjssalhdgckzfihvtsywomu",
            "pnebjqjalrdgckzfiavtxywoku",
            "vnebjqsalrdgckzfihvbxmwomu",
            "phebjksaurdgckzfihvtxywomu",
            "pneojqealrdgckzhihvtxywomu",
            "snebjqsalrdgckzqihvtxyzomu",
            "pnebjqsalrtackzfihvtxswomu",
            "bnebjqlalrdgckzfihvtxywhmu",
            "pnebjqfalrdgckzfijvtxywomi",
            "fnehjbsalrdgckzfihvtxywomu",
            "pnebjasalrdgckzdihvtxqwomu",
            "pnebjhsaljdgckzfihvtxywmmu",
            "pnebjqsalrdgckzfihvsxykoau",
            "pnebjqsalrdgckzbihvtdywomc",
            "pnobjqsalrxgckzfihvtxywomh",
            "pnebjqstlrdgchzfihvtxywnmu",
            "pnebjquaxrdgckzfihvtxywolu",
            "pqebjqsalrdgcdzfihvtcywomu",
            "xnabjqsalrdgckzfihvtxywmmu",
            "rnebjqsalrdgckzfihvtxmwouu",
            "vaebjqsalrdgckcfihvtxywomu",
            "pnebjqsalrpgcnzfihvbxywomu",
            "pcvbjqsalrdjckzfihvtxywomu",
            "pneyjqsafrdgckzfihdtxywomu",
            "pxedjqsalrdgckzfihvtxyzomu",
            "pnebjqsalrdgctzfihnyxywomu",
            "pnebjqsalrdgckzfihvtnylsmu",
            "pnebjqsalrdyckzfihvbxycomu",
            "fnebjqsalrdgckzfihvtxtwomc",
            "pnobjqsalrdgckdfihvtxywomh",
            "pqebjqsalrdgcqzfihvtxywymu",
            "pnebxqsalrdgckzficvtwywomu",
            "pnebjqshlragczzfihvtxywomu",
            "pnebqqsalrdackzfihttxywomu",
            "pnebjqsalrdsckwfbhvtxywomu",
            "pnehjqsalrdgcuzfxhvtxywomu",
            "pnebjqsavrdgckzfihvexywomn",
            "pnebunsalrdgckzfihvtxywomi",
            "pnebjxsalrdgckzfmhvtpywomu",
            "rnebjqsalrdghkzfihztxywomu",
            "pnebjqsalrigcbzfihvfxywomu",
            "pnebqqsalrggckzfihvtxyromu",
            "pnebjqsalrdgchzfihvtxylmmu",
            "pnebeqsalrdgckzdihvtxywoms",
            "pnebjqsalrdgckzzihvfxywozu",
            "pnzbjgsalrtgckzfihvtxywomu",
            "pnebjqsaledgckzjihvtxzwomu",
            "pnebjqsalydgckqfihvtxywouu",
            "pnebjqsalrdgckufihvqxdwomu",
            "pnebjqsylrdgckzfihvdxyjomu",
            "pnemjqsalrdgckzeihvtxywoqu",
            "plebjasalrdgckzfihvtxywomb",
            "pnebjqsadrdgckufihvtxyfomu",
            "pbebjqsaardgckzfihvtxmwomu",
            "pnebjqsalrdgcmzfihotxywgmu",
            "pnebjqsaprdgcizfihvtxywhmu",
            "pnebjqsalrkgcuzfihvtlywomu",
            "pnebjqsalrdnckzfihvtxysomg",
            "pnebjqdafrdgckzfihctxywomu",
            "pnebjqsalrdgckzfihutxkwomp",
            "pnebvqsalrdgclzfimvtxywomu",
            "pnebjqralrdgcktfihvtxiwomu",
            "pneujqsalrdsckzfzhvtxywomu",
            "pnebfqgalrdgckzfihvtxywjmu",
            "pneyjqsalrkgckzfihctxywomu",
            "pndbjqsalrdgckzfjhvtxywouu",
            "pneljnsalrdgcozfihvtxywomu",
            "phebjqsalrdgckzfihxtxdwomu",
            "pnlbjqsalrhgckzfzhvtxywomu",
            "pnebjqsalrsgckzfiovtxywwmu",
            "pncbjqsalrdgfkzfivvtxywomu",
            "nnebjqsalrdgckzfthvtxycomu",
            "pnebjqwalsdgckzfixvtxywomu",
            "pnebjtsalrdgcfzfimvtxywomu",
            "pnebjqsvlrdgckzfihutxfwomu",
            "pnebjmsalrdgckzkxhvtxywomu",
            "pnekjqsllrdgckzfinvtxywomu",
            "pneijqsxlrdgckzfihvtxywjmu",
            "wnxbjqsafrdgckzfihvtxywomu",
            "pnebjqskledgokzfihvtxywomu",
            "pnebjqvalrdgckzfihvtxytoju",
            "pneqjqsalrdgckzfilvthywomu",
            "pnebjqsalrdgckzfihvokywomf",
            "bnebjqsalrdgckufihvtxywimu",
            "pnebjqsaurdgckzfihvtrywosu",
            "pnebjmsaludgckzfihvtxywomn",
            "pnebdqsalrdgcktfihvtxywodu",
            "pnebjqjylzdgckzfihvtxywomu",
            "piebjqsalrdgcrzfihstxywomu",
            "pnebjqsaurdgckwfnhvtxywomu",
            "pnebxqsajrdgcjzfihvtxywomu",
            "pnebjqsalrdghsdfihvtxywomu",
            "pnebcqsxlrdgckzfihvtxyaomu",
            "pnefjqsalrdgckzfuhvtxyworu",
            "pnebjqsalrdlcksfihvteywomu",
            "pnebjqlalrgackzfihvtxywomu",
            "pnebdqsalrdickzfihvtxdwomu",
            "pneujksalrdgctzfihvtxywomu",
            "pnebjqsalrduckzfihvsxywomf",
            "pnebjqsalrdgckcfihotxywomd",
            "envbjqsalsdgckzfihvtxywomu",
            "pnebjqsalzdgcvzzihvtxywomu",
            "pnebjqsalrdyckzflhvyxywomu",
            "pnebjqsalrdglkzfihstxymomu",
            "pnebmqsalrdgokzfihvtxywoml",
            "pnebjqsylrdnckzfihatxywomu",
            "pnebjqaflndgckzfihvtxywomu",
            "pneboqsagragckzfihvtxywomu",
            "peebjqstlndgckzfihvtxywomu",
            "onebjqsklrdgckzfihvtxmwomu",
            "pnebjqjnlrdgckrfihvtxywomu",
            "pnebjqsalrhgckzfihvqxywomh",
            "pnebjqsalrdgckzzihvtxowomw",
            "pnebjgsalrdgckffihltxywomu",
            "znebaqsalcdgckzfihvtxywomu",
            "pnnbjqeasrdgckzfihvtxywomu",
            "rnebjqaalrxgckzfihvtxywomu",
            "pnebjqsalrdgckaxphvtxywomu",
            "pnebjcnalrdgnkzfihvtxywomu",
            "pnebjasalbdgckzmihvtxywomu",
            "pnebjqsalrdgckefjhvtmywomu",
            "pnebjqsalrdgmkzfihvtxyoomb",
            "pnebjqsalrkgckogihvtxywomu",
            "pnwbjqsalrdgckztihvtxywomt",
            "pnebjqsalrdgckzfihotgnwomu",
            "pnebjqsdlrrgckzfihvtxyaomu",
            "pnebvasalrdgckzfihvtsywomu",
            "pnebrqqalrvgckzfihvtxywomu",
            "tnebjqsglrdgqkzfihvtxywomu",
            "pnebjqsatrsgckifihvtxywomu",
            "pneboqsalrdgckzfihvkxywomi",
            "pnezaqsalrdgcktfihvtxywomu",
            "pnebjqsnlrdgckzfihvfxqwomu",
            "pneajqsaxrmgckzfihvtxywomu",
            "pnebjosalodgckzfihvxxywomu",
            "pnebjqsalndgckmfihvtfywomu",
            "pneejqsalidgckzfihgtxywomu",
            "pnecjqsalrdgckzfihptxiwomu",
            "tnebjqsalrdgckznihvxxywomu",
            "ptebjqsalrdgckzfimvtxywomm",
            "wnebjqsalndgckzfihvtxywoju",
            "fnebmqsplrdgckzfihvtxywomu",
            "pnlbjqsalrdghkzficvtxywomu",
            "pnebjqsesrdgckzdihvtxywomu",
            "pnebjqsalregokzfirvtxywomu",
            "pnebjtualrtgckzfihvtxywomu",
            "pnebjwsdlrdgckzfihvtxywoml",
            "pnlbjqsayrdgckzfqhvtxywomu",
            "pnebjwsalpdgckzfihvtxywomc",
            "pnqbjqsalcdgckzhihvtxywomu",
            "pneujqsalrdgckzfhhvtxrwomu",
            "pnebjqsalqdgcizfihvtxywimu",
            "pnebjqsacldgckzfihvwxywomu",
            "puebjqsalrdgckzfbhvtxyeomu",
            "pnebjqsalrdgcyimihvtxywomu",
            "pnebjlsalrdgckzfihvtxiwome",
            "pnebfusalrdgckzfihvtxywodu",
            "pnebjqsalrdgvazfirvtxywomu",
            "pnebjqsalrdgckyfohvtxywomz",
            "gnenjqsalrdgckzfihvtxynomu",
            "mnebjqsalrdgckhfihvtxycomu",
            "phebjqsalrdgckzfihvtxtworu",
            "pnebjqsalrdgdkzfihvtxywfmj",
            "pneveqsairdgckzfihvtxywomu",
            "pnebjqsalcdlckzfihvtxywomg",
            "pneajqsalrdgckzfihvtxygoxu",
            "puebjqdclrdgckzfihvtxywomu",
            "tuebjqsalrdgckzfihvtxywoou",
            "pwenjqsalrdgckzfihvtxywomg",
            "pnebjqsalrdgckzfihhltywomu",
            "pnebjqsalrdgchzqievtxywomu",
            "pnegjqsalrdgckzfiovtxywdmu",
            "pnebjaralrqgckzfihvtxywomu",
            "pnebjqsalrdrckzfimvtxywomm",
            "pnebjqsalrdgckzfpgvtxewomu",
            "pnebjqsalrdhcqzfihitxywomu",
            "pnebjqsalrjgckefihmtxywomu",
            "pnebjcsalrdgcksfikvtxywomu",
            "pnebjqsalrdgckzfihvtxywdjc",
            "pnebjqsazrjgckzjihvtxywomu",
            "pnfbjqsclrdgckzfihvtxybomu",
            "pnebjqsalrdgckuqihvtxyaomu",
            "pfpbjzsalrdgckzfihvtxywomu",
            "pnevjqsalrdgckwfihytxywomu",
            "pnebjqsqlrkgckzfihvtvywomu",
            "pneejqsalrdlckzfihvtxywopu",
            "pnebjqsalcdgxkzfihvtxywomd",
            "pneqjqsalrdgcvzzihvtxywomu",
            "pnvbjqsalydgctzfihvtxywomu",
            "pnebjqsalrdgckzzihvfxywomn",
            "pnybjqsaerdgckzfihstxywomu",
            "pnobjqsalrdkckzfihvtxywomv",
            "pnebjqsalridckzfihvtxywfmu",
            "pnhbjqsaludgckyfihvtxywomu",
            "pnetjqsaprdgykzfihvtxywomu",
            "wnebjqsalrdvcfzfihvtxywomu",
            "pnetjqsalrdmckwfihvtxywomu",
            "pnebjysalrdgcszfihvtxnwomu",
            "pnebjqsrlrdgckzfihvtxywkhu",
            "pnubjqsplrdgcjzfihvtxywomu",
            "pnebjqsalrdzckzficjtxywomu",
            "pnebjqsalregckzfinvtxywoku",
            "pnebjqsalrcgckyfivvtxywomu",
            "pyenjqsalrdgckzfihvnxywomu",
            "prebjqsalrdnckzfihvtxysomg",
            "pnebjnsalrdgchzfihvaxywomu",
            "pnebjqsalrdgckzfihxagywomu",
            "pnebjqsalrdgckzvihvtoywoml",
            "pnebjqsilrdgckzfihvtfywgmu",
            "pnebjqmalrdgckzfihvtvawomu",
            "pnebqqsalrdgckzfiuvtfywomu",
            "pneqjqsalrdgckzfihvqxywomi",
            "pnebjesalrsgckzfihvtxywmmu",
            "znebjqsblrdgckzfihvlxywomu",
            "pnebjqsalrdgckzfuhvtlyworu",
            "pnebjqsylrdgckzfihvqxpwomu",
            "onebjqsalfdgckifihvtxywomu",
            "pnebjusalrdgckzfihvtxywyml",
            "pnebjssflrdgckzfigvtxywomu",
            "pnebjfsdzrdgckzfihvtxywomu",
            "pnebjqsalrdgcktfihvixywocu",
            "gnebjqnaqrdgckzfihvtxywomu",
            "pnebjqsaqrugckzfihhtxywomu",
            "pnebjqsxlrdgckzfihvtxlwosu",
            "pnebjzsalrdgckzmihvtxywovu",
            "pnebgqsalrdgckzfizvtxyjomu",
            "pnebjqsmlrdgckzfihvtxywsmi",
            "pnebjqsakmdgckzjihvtxywomu",
            "pnebjqdglrdgckvfihvtxywomu",
            "pnebmhsalrdgckxfihvtxywomu",
            "pneejqsalrdlckzfihvnxywomu",
            "bnebjqsalmdgckzfihvfxywomu",
            "bnebjnsalrdgcizfihvtxywomu",
            "pnebjqsalhdgcdzfihvbxywomu",
            "pnebjqsjlrdgckzfihvgiywomu",
            "pnebjisalrdgckzfihvtxywqmi",
            "pdebjqsalrdickzfihhtxywomu",
            "pnebjqsalrdkckzfihvjeywomu",
            "pneyjqsalrqgckzfihvtxywohu",
            "pnebjqsalrdgckcfihvtxjlomu",
            "plebqwsalrdgckzfihvtxywomu",
            "pnebjqlalrdgckzfihetxynomu",
            "sngbjqsalrdgckzfihvmxywomu"

        };
    }
}
