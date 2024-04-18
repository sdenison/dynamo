using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.Casino.Slots;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Dynamo.Business.Unit.Tests.Casino.Slots
{
    public class MechanicalSlotMachineTests
    {

        [Test]
        public void Can_create_a_reel()
        {
            var reelString = ReelStrings()[0];
            var reel = new Reel(reelString);
            Assert.That(reel.CurrentSymbol.Symbol, Is.EqualTo(Symbol.Butterfly));
            Assert.That(reel.CurrentSymbol.NextSymbol.Symbol, Is.EqualTo(Symbol.RedScarf));
            Assert.That(reel.CurrentSymbol.PreviousSymbol.Symbol, Is.EqualTo(Symbol.QuillAndInk));

            //Move to the next symbol
            reel.Next();
            Assert.That(reel.CurrentSymbol.Symbol, Is.EqualTo(Symbol.RedScarf));
            Assert.That(reel.CurrentSymbol.NextSymbol.Symbol, Is.EqualTo(Symbol.Guitar));
            Assert.That(reel.CurrentSymbol.PreviousSymbol.Symbol, Is.EqualTo(Symbol.Butterfly));
        }

        [Test]
        public void Can_create_a_mechanical_slot_machine()
        {
            var slotMachine = new MechanicalSlotMachine(ReelStrings(), GetPayouts());
        }

        public List<Payout> GetPayouts()
        {
            var oneHundredPayout = new Payout(new List<Symbol> { Symbol.QuillAndInk }, 100);
            var fiftyPayout = new Payout(new List<Symbol> { Symbol.RedScarf }, 50);
            var tenPayout = new Payout(new List<Symbol> { Symbol.HeartHands, Symbol.Thirteen, Symbol.RedScarf, Symbol.RedLips, Symbol.StatueOfLiberty, Symbol.Butterfly, Symbol.Cardigan, Symbol.Champagne, Symbol.FriendshipBracelet }, 50);
            var fivePayout = new Payout(new List<Symbol> { Symbol.HeartHands, Symbol.Thirteen, Symbol.RedLips, Symbol.RedScarf, Symbol.StatueOfLiberty }, 5);
            var zeroPayout = new Payout(new List<Symbol> { Symbol.Guitar, Symbol.Snake }, 0);
            return new List<Payout>()
            {
                oneHundredPayout,
                fiftyPayout,
                tenPayout,
                fivePayout,
                zeroPayout
            };
        }

        public List<string> ReelStrings()
        {
            return new List<string>
            {
                "Butterfly, Red Scarf, Guitar, Red Scarf, Cardigan, Snake, Butterfly, Heart hands, Butterfly, Red Lips, 13, Red Lips, 13, Cardigan, Friendship Bracelet, Red Lips, Red Scarf, Heart hands, Snake, Quill and Ink",
                "Heart hands, Snake, Champagne, Cardigan, Quill and Ink, Cardigan, Cardigan, Snake, Red Scarf, Snake, Champagne, Friendship Bracelet, Snake, Butterfly, Red Scarf, Butterfly, 13, Snake, Cardigan, Champagne",
                "Snake, 13, Friendship Bracelet, Red Scarf, Guitar, Red Scarf, Cardigan, 13, Guitar, Red Lips, Red Lips, Red Lips, Quill and Ink, Cardigan, Champagne, Red Scarf, Cardigan, Red Scarf, Statue of Liberty, Heart hands"
            };
        }

    }
}
