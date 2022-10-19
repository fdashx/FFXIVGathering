using FFXIVGathering.Calc.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Tests
{
    [TestFixture]
    internal class OutcomeComparerTests
    {
        [TestCase(50, 200, 40, 150, 1)]
        [TestCase(40, 200, 50, 150, -1)]
        [TestCase(50, 200, 50, 150, -1)]
        [TestCase(50, 150, 50, 200, 1)]
        [TestCase(50, 200, 50, 200, 0)]
        [TestCase(50, 200, 49.9999999, 200, 0)]
        [TestCase(50, 200, 49.9999999, 100, -1)]
        public void YieldComparerTest(double xYield, int xGP, double yYield, int yGP, int expected)
        {
            var x = new GatheringOutcome { Yield = xYield, UsedGP = xGP };
            var y = new GatheringOutcome { Yield = yYield, UsedGP = yGP };

            var actual = new GatheringYieldComparer().Compare(x, y);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void YieldComparerNullTest()
        {
            var target = new GatheringYieldComparer();
            var outcome = new GatheringOutcome();

            Assert.Multiple(() =>
            {
                Assert.That(target.Compare(null, null), Is.EqualTo(0));
                Assert.That(target.Compare(null, outcome), Is.EqualTo(-1));
                Assert.That(target.Compare(outcome, null), Is.EqualTo(1));
            });
        }

        [TestCase(20.5, 0.2, 18.3, 0.1, 1)]
        [TestCase(20.5, 0.2, 18.3, 0.3, -1)]
        [TestCase(20.5, 0.2, 18.3, 0.2, 1)]
        [TestCase(20.5, 0.2, 20.8, 0.2, -1)]
        [TestCase(20.5, 0.2, 20.5, 0.2, 0)]
        [TestCase(20.5, 0.1999999, 20.5, 0.2, 0)]
        [TestCase(22.3, 0.1999999, 20.5, 0.2, 1)]
        [TestCase(20.4999999, 0.2, 20.5, 0.2, 0)]
        public void EfficiencyComparerTest(double xYield, double xAddYield, double yYield, double yAddYield, int expected)
        {
            var x = new GatheringOutcome { Yield = xYield, AddYieldPerGP = xAddYield };
            var y = new GatheringOutcome { Yield = yYield, AddYieldPerGP = yAddYield };

            var actual = new GatheringEfficiencyComparer().Compare(x, y);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void EfficiencyComparerNullTest()
        {
            var target = new GatheringEfficiencyComparer();
            var outcome = new GatheringOutcome();

            Assert.Multiple(() =>
            {
                Assert.That(target.Compare(null, null), Is.EqualTo(0));
                Assert.That(target.Compare(null, outcome), Is.EqualTo(-1));
                Assert.That(target.Compare(outcome, null), Is.EqualTo(1));
            });
        }
    }
}
