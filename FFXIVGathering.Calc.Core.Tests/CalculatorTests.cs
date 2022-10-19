using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using FFXIVGathering.Calc.Core.Models;
using Moq;
using System.Threading.Channels;

namespace FFXIVGathering.Calc.Core.Tests
{
    [TestFixture]
    public class GatheringCalculatorTests
    {
        private static IGatheringCalculator GetTarget()
        {
            return new GatheringCalculator();
        }

        private static GatheringContext GetDefaultContext()
        {
            return new GatheringContext
            {
                BaseAmount = 1,
                Chance = 1,
                Attempts = 4,
                Boon = 0,
                BountifulBonus = 3,
                BoonBonus = 1,
                BountifulAttempts = 0,
                WiseAttempts = 0
            };
        }

        [TestCase(0, 0)]
        [TestCase(1, 4)]
        [TestCase(0.5, 2)]
        public void ChanceTest(double chance, double expected)
        {
            var target = GetTarget();
            var context = GetDefaultContext();
            context.Chance = chance;

            var actual = target.CalculateYield(context);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(0, 2, 4)]
        [TestCase(1, 2, 12)]
        [TestCase(0.6, 1, 6.4)]
        public void BoonTest(double boon, int boonBonus, double expected)
        {
            var target = GetTarget();
            var context = GetDefaultContext();
            context.Boon = boon;
            context.BoonBonus = boonBonus;

            var actual = target.CalculateYield(context);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(4, 0, 4)]
        [TestCase(5, 1, 5.5)]
        [TestCase(6, 2, 7)]
        public void WiseToTheWorldTest(int attempts, int wiseAttempts, double expected)
        {
            var target = GetTarget();
            var context = GetDefaultContext();
            context.Attempts = attempts;
            context.WiseAttempts = wiseAttempts;

            var actual = target.CalculateYield(context);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(4, 0, 1, 1, 5)]
        [TestCase(4, 0, 3, 4, 16)]
        [TestCase(4, 0, 3, 5, 16)]
        [TestCase(5, 1, 1, 6, 11)]
        [TestCase(6, 2, 1, 7, 13.75)]
        [TestCase(8, 4, 2, 10, 29.25)]
        public void BountifulTest(int attempts, int wiseAttempts, int bountifulBonus, int bountifulAttempts, double expected)
        {
            var target = GetTarget();
            var context = GetDefaultContext();
            context.Attempts = attempts;
            context.WiseAttempts = wiseAttempts;
            context.BountifulBonus = bountifulBonus;
            context.BountifulAttempts = bountifulAttempts;

            var actual = target.CalculateYield(context);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(new int[] {100, 50, 200})]
        public void GPTest(int[] costs)
        {
            var target = GetTarget();
            var actions = new List<IGatheringAction>();

            foreach (var c in costs)
            {
                var actionMock = new Mock<IGatheringAction>();
                actionMock.Setup(a => a.GP).Returns(c);
                actions.Add(actionMock.Object);
            }

            var actual = target.CalculateGP(actions);

            Assert.That(actual, Is.EqualTo(costs.Sum()));
        }
    }
}