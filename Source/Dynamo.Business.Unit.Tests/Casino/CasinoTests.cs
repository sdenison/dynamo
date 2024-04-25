using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Business.Shared.Casino;
using NUnit.Framework;

namespace Dynamo.Business.Unit.Tests.Casino
{
    public class CasinoTests
    {
        [Test]
        public void Can_create_a_casino()
        {
            var casino = new Shared.Casino.Casino();
            int n = 11040; //This is 460 per hour for 24 hours
            double scale = 2; //This is the average of how long they spend in the casino

            double[] timeInCasino = MathHelper.GenerateExponentialRandomVariables(scale, n);



            for (var i = 0; i < timeInCasino.Length; i++)
            {
                var x = timeInCasino[i];
            }

            casino.TotalTime = 24; //24 hours in seconds

            var numbersOfUsers = new List<int>();

            var playerNum = 0;
            while (casino.CurrentTime < casino.TotalTime && playerNum < timeInCasino.Length)
            {
                var player = new Player() { TimeInCasino = timeInCasino[playerNum] };
                casino.AddPlayer(player);
                playerNum++;

                if (casino.CurrentTime > 12)
                {
                    var numberOfUsers = casino.Players.Count;
                    numbersOfUsers.Add(numberOfUsers);
                }
            }

            var averageNumberOfUsers = numbersOfUsers.Sum(x => x) / numbersOfUsers.Count;
            //Accepted answer was 927
            Assert.That(averageNumberOfUsers, Is.GreaterThan(908));
            Assert.That(averageNumberOfUsers, Is.LessThan(934));

            //Assert.That(timeInCasino.Length, Is.EqualTo(0));

        }
    }
}
