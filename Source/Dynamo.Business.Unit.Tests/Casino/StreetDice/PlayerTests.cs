using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dynamo.Business.Shared.Casino.StreetDice;

namespace Dynamo.Business.Unit.Tests.Casino.StreetDice
{
    public class PlayerTests
    {
        [Test]
        public void Can_parse_player_string()
        {
            var playerString = "1: $40 50% - 4,3";
            var player = Player.Parse(playerString);
            Assert.That(player.PlayerId, Is.EqualTo(1));
            Assert.That(player.CurrentMoney, Is.EqualTo(40));
            Assert.That(player.MaximumPercentage, Is.EqualTo(0.50));
        }
    }
}
