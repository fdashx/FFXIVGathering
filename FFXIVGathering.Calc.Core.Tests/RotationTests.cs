using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using FFXIVGathering.Calc.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Tests
{
    [TestFixture]
    internal class RotationTests
    {
        [Test]
        public void AddActionExecuteTest()
        {
            var context = new GatheringContext();
            var actionMock = new Mock<IGatheringAction>();
            var target = new Rotation(context);

            target.AddAction(actionMock.Object);

            actionMock.Verify(a => a.Execute(context));
            Assert.That(target.Actions, Contains.Item(actionMock.Object));
        }
    }
}
