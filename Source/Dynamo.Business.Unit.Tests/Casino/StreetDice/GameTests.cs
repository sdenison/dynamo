using Dynamo.Business.Shared.Casino.StreetDice;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Casino.StreetDice
{
    public class GameTests
    {
        [Test]
        public void Can_load_game_from_list_of_players()
        {
            var playerStrings = TestDataProvider.GetPlayerStrings();
            var game = Game.Parse(playerStrings);

            Assert.That(game.Players.Count, Is.EqualTo(500));
            //Spot Checking that players loaded correctly
            Assert.That(game.Players[50].PlayerId, Is.EqualTo(51));
            Assert.That(game.Players[50].Nemeses[3].PlayerId, Is.EqualTo(102));
        }
    }
}
