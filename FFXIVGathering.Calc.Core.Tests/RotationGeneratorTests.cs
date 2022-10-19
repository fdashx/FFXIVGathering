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
    internal class RotationGeneratorTests
    {
        private static RotationGenerator GetTarget(IRotationFactory rotationFac, IList<IGatheringAction> availableActions)
        {
            return new RotationGenerator(rotationFac, availableActions);
        }

        private static IGatheringAction MockAction(bool canExecute)
        {
            var actionMock = new Mock<IGatheringAction>();
            actionMock.Setup(a => a.CanExecute(It.IsAny<GatheringContext>())).Returns(canExecute);
            return actionMock.Object;
        }

        private void MockInitialRotation(GatheringContext initContext, out Mock<IRotation> emptyRotationMock, out Mock<IRotationFactory> factoryMock)
        {
            emptyRotationMock = new Mock<IRotation>();
            emptyRotationMock.Setup(r => r.Context).Returns(initContext);
            factoryMock = new Mock<IRotationFactory>();
            factoryMock.Setup(f => f.CreateRotation(initContext)).Returns(emptyRotationMock.Object);
        }

        [Test]
        public void NoActionsTest()
        {
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction>());
            

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(1));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            rotationFacMock.Verify(f => f.CreateRotation(initContext), Times.Once());
            rotationFacMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SingleActionTest()
        {
            var action = MockAction(true);
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var singleActionRotationMock = new Mock<IRotation>();
            rotationFacMock
                .Setup(f => f.Copy(emptyRotationMock.Object))
                .Returns(singleActionRotationMock.Object).Verifiable();
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction> { action });

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(2));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            Assert.That(rotations, Has.Member(singleActionRotationMock.Object));
            rotationFacMock.Verify();
            singleActionRotationMock.Verify(r => r.AddAction(action), Times.Once);
        }

        [Test]
        public void SingleActionNotExecutableTest()
        {
            var action = MockAction(false);
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction> { action });

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(1));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            emptyRotationMock.Verify(r => r.Context);
            rotationFacMock.Verify(f => f.CreateRotation(initContext), Times.Once());
            rotationFacMock.VerifyNoOtherCalls();
        }

        [Test]
        public void MultipleActionsTest()
        {
            var action1 = MockAction(true);
            var action2 = MockAction(true);
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var rotationMocks = new List<Mock<IRotation>>();
            var action1RotationMock = new Mock<IRotation>();
            var action2RotationMock = new Mock<IRotation>();
            var bothActionRotationMock = new Mock<IRotation>();
            rotationFacMock
                .SetupSequence(f => f.Copy(emptyRotationMock.Object))
                .Returns(action1RotationMock.Object)
                .Returns(action2RotationMock.Object);
            rotationFacMock
                .Setup(f => f.Copy(action1RotationMock.Object))
                .Returns(bothActionRotationMock.Object).Verifiable();
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction> { action1, action2 });

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(4));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            Assert.That(rotations, Has.Member(action1RotationMock.Object));
            Assert.That(rotations, Has.Member(action2RotationMock.Object));
            Assert.That(rotations, Has.Member(bothActionRotationMock.Object));
            action1RotationMock.Verify(r => r.AddAction(action1), Times.Once);
            action2RotationMock.Verify(r => r.AddAction(action2), Times.Once);
            bothActionRotationMock.Verify(r => r.AddAction(action2), Times.Once);
            rotationFacMock.Verify();
        }

        [Test]
        public void MultipleActionsNotExecutableTest()
        {
            var action1 = MockAction(false);
            var action2 = MockAction(true);
            var action3 = MockAction(false);
            var action4 = MockAction(true);
            var action5 = MockAction(false);
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var rotationMocks = new List<Mock<IRotation>>();
            var action2RotationMock = new Mock<IRotation>();
            var action4RotationMock = new Mock<IRotation>();
            var bothActionRotationMock = new Mock<IRotation>();
            rotationFacMock
                .SetupSequence(f => f.Copy(emptyRotationMock.Object))
                .Returns(action2RotationMock.Object)
                .Returns(action4RotationMock.Object);
            rotationFacMock
                .Setup(f => f.Copy(action2RotationMock.Object))
                .Returns(bothActionRotationMock.Object).Verifiable();
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction> { action1, action2, action3, action4, action5 });

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(4));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            Assert.That(rotations, Has.Member(action2RotationMock.Object));
            Assert.That(rotations, Has.Member(action4RotationMock.Object));
            Assert.That(rotations, Has.Member(bothActionRotationMock.Object));
            action2RotationMock.Verify(r => r.AddAction(action2), Times.Once);
            action4RotationMock.Verify(r => r.AddAction(action4), Times.Once);
            bothActionRotationMock.Verify(r => r.AddAction(action4), Times.Once);
            rotationFacMock.Verify();
        }

        [Test]
        public void RepeatableActionTest()
        {
            var repeats = 3;
            var actionMock = new Mock<IGatheringAction>();
            actionMock.Setup(a => a.CanExecute(It.IsAny<GatheringContext>())).Returns(() => repeats > 0);
            actionMock.Setup(a => a.IsRepeatable).Returns(true);
            var initContext = new GatheringContext();
            MockInitialRotation(initContext, out var emptyRotationMock, out var rotationFacMock);
            var rotationMocks = new List<Mock<IRotation>>();
            var repeat1RotationMock = new Mock<IRotation>();
            repeat1RotationMock.Setup(r => r.AddAction(actionMock.Object)).Callback(() => repeats--).Verifiable();
            var repeat2RotationMock = new Mock<IRotation>();
            repeat2RotationMock.Setup(r => r.AddAction(actionMock.Object)).Callback(() => repeats--).Verifiable();
            var repeat3RotationMock = new Mock<IRotation>();
            repeat3RotationMock.Setup(r => r.AddAction(actionMock.Object)).Callback(() => repeats--).Verifiable();
            rotationFacMock
                .Setup(f => f.Copy(emptyRotationMock.Object))
                .Returns(repeat1RotationMock.Object).Verifiable();
            rotationFacMock
                .Setup(f => f.Copy(repeat1RotationMock.Object))
                .Returns(repeat2RotationMock.Object).Verifiable();
            rotationFacMock
                .Setup(f => f.Copy(repeat2RotationMock.Object))
                .Returns(repeat3RotationMock.Object).Verifiable();
            var target = GetTarget(rotationFacMock.Object, new List<IGatheringAction> { actionMock.Object });

            var rotations = target.GetRotations(initContext);

            Assert.That(rotations, Has.Count.EqualTo(4));
            Assert.That(rotations[0], Is.EqualTo(emptyRotationMock.Object));
            Assert.That(rotations, Has.Member(repeat1RotationMock.Object));
            Assert.That(rotations, Has.Member(repeat2RotationMock.Object));
            Assert.That(rotations, Has.Member(repeat3RotationMock.Object));
            repeat1RotationMock.Verify();
            repeat2RotationMock.Verify();
            repeat3RotationMock.Verify();
            rotationFacMock.Verify();
        }
    }
}
