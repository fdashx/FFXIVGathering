using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Models
{

    public class RotationGenerator : IRotationGenerator
    {
        private readonly IRotationFactory _rotationFactory;
        private readonly IList<IGatheringAction> _availableActions;
        private readonly List<IRotation> _rotations;

        public RotationGenerator(IRotationFactory roationFactory, IList<IGatheringAction> availableActions)
        {
            _rotationFactory = roationFactory;
            _availableActions = availableActions;
            _rotations = new List<IRotation>();
        }

        public List<IRotation> GetRotations(GatheringContext initialContext)
        {
            _rotations.Clear();
            var emptyRotation = _rotationFactory.CreateRotation(initialContext);
            _rotations.Add(emptyRotation);
            NextRotation(0, emptyRotation);
            return _rotations;
        }

        private void NextRotation(int currActionIdx, IRotation currRotation)
        {
            if (currActionIdx >= _availableActions.Count)
            {
                return;
            }

            var currAction = _availableActions[currActionIdx];

            if (currAction.IsRepeatable)
            {
                // generate a rotation each time action is repeated
                var actionRepeatedRotation = currRotation;

                while (currAction.CanExecute(actionRepeatedRotation.Context))
                {
                    actionRepeatedRotation = _rotationFactory.Copy(actionRepeatedRotation);
                    actionRepeatedRotation.AddAction(currAction);
                    _rotations.Add(actionRepeatedRotation);
                    NextRotation(currActionIdx + 1, actionRepeatedRotation);
                }
            }
            else if (currAction.CanExecute(currRotation.Context))
            {
                // generate rotations with action executed
                var actionExectuedRotation = _rotationFactory.Copy(currRotation);
                actionExectuedRotation.AddAction(currAction);
                _rotations.Add(actionExectuedRotation);
                NextRotation(currActionIdx + 1, actionExectuedRotation);
            }

            // generate rotations without action executed
            NextRotation(currActionIdx + 1, currRotation);
        }
    }
}
