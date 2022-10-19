using FFXIVGathering.Calc.Core.Actions;
using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Tests
{
    [TestFixture]
    public class ActionsTests
    {
        private void TestChance(IGatheringAction chanceAction)
        {
            var context = new GatheringContext() { Chance = 1, };

            chanceAction.Execute(context);

            Assert.That(context.Chance, Is.LessThanOrEqualTo(1));
        }

        private void TestBoon(IGatheringAction boonAction)
        {
            var context = new GatheringContext() { Boon = 1, };

            boonAction.Execute(context);

            Assert.That(context.Boon, Is.LessThanOrEqualTo(1));
        }

        [TestCase(30, 35, false)]
        [TestCase(30, 25, true)]
        [TestCase(30, 30, true)]
        public void CanExecuteCheckLevelTest(int level, int reqLevel, bool expected)
        {
            var actionMock = new Mock<BaseAction>() { CallBase = true };
            actionMock.Setup(a => a.Level).Returns(reqLevel);
            var context = new GatheringContext() { CharacterLevel = level };

            var actual = actionMock.Object.CanExecute(context);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(200, 500, false)]
        [TestCase(200, 100, true)]
        [TestCase(200, 200, true)]
        public void CanExecuteCheckGPTest(int gp, int reqGP, bool expected)
        {
            var actionMock = new Mock<BaseAction>() { CallBase = true };
            actionMock.Setup(a => a.GP).Returns(reqGP);
            var context = new GatheringContext() { AvailableGP = gp };

            var actual = actionMock.Object.CanExecute(context);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ExecuteDecreaseGPTest()
        {
            var actionMock = new Mock<BaseAction>() { CallBase = true };
            actionMock.Setup(a => a.GP).Returns(100);
            var context = new GatheringContext() { AvailableGP = 250};

            actionMock.Object.Execute(context);

            Assert.That(context.AvailableGP, Is.EqualTo(150));
        }

        [Test]
        public void ChanceIMax100PercTest()
        {
            TestChance(new Chance1());
        }

        [Test]
        public void ChanceIIMax100PercTest()
        {
            TestChance(new Chance2());
        }

        [Test]
        public void ChanceIIIMax100PercTest()
        {
            TestChance(new Chance3());
        }

        [Test]
        public void GiftIMax100PercTest()
        {
            TestBoon(new Gift1());
        }

        [Test]
        public void GiftIIMax100PercTest()
        {
            TestBoon(new Gift2());
        }
    }
}
