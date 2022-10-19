using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Models
{
    public class RotationFactory : IRotationFactory
    {
        public IRotation CreateRotation(GatheringContext context)
        {
            return new Rotation(context);
        }

        public IRotation Copy(IRotation rotation)
        {
            var copiedContext = new GatheringContext
            {
                Attempts = rotation.Context.Attempts,
                AvailableGP = rotation.Context.AvailableGP,
                BaseAmount = rotation.Context.BaseAmount,
                Boon = rotation.Context.Boon,
                BoonBonus = rotation.Context.BoonBonus,
                BountifulAttempts = rotation.Context.BountifulAttempts,
                BountifulBonus = rotation.Context.BountifulBonus,
                Chance = rotation.Context.Chance,
                CharacterLevel = rotation.Context.CharacterLevel,
                WiseAttempts = rotation.Context.WiseAttempts,
            };
            var copiedRotation = new Rotation(copiedContext);
            
            foreach (var action in rotation.Actions)
            {
                copiedRotation.Actions.Add(action);
            }

            return copiedRotation;
        }
    }
}
