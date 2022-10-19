using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using MathNet.Numerics.Distributions;

namespace FFXIVGathering.Calc.Core.Models
{

    public class GatheringCalculator : IGatheringCalculator
    {
        public int CalculateGP(IEnumerable<IGatheringAction> actions)
        {
            return actions.Sum(a => a.GP);
        }

        public GatheringOutcome CalculateOutcome(IRotation rotation, GatheringOutcome? baseOutcome = null)
        {
            var outcome = new GatheringOutcome() 
            { 
                Yield = CalculateYield(rotation.Context),
                UsedGP = CalculateGP(rotation.Actions)
            };

            if (baseOutcome != null && outcome.UsedGP > 0)
            {
                outcome.AddYieldPerGP = (double)(outcome.Yield - baseOutcome.Yield) / outcome.UsedGP;
            }

            return outcome;
        }

        public double CalculateYield(GatheringContext context)
        {
            var binom = new Binomial(0.5, context.WiseAttempts);
            var avgYield = 0d;

            for (var i = 0; i <= context.WiseAttempts; i++)
            {
                var wiseSucessProb = binom.Probability(i);
                var attempts = context.Attempts + i;
                avgYield += ((context.BaseAmount + context.Boon * context.BoonBonus) * attempts + context.BountifulBonus * Math.Min(attempts, context.BountifulAttempts)) * context.Chance * wiseSucessProb;
            }

            return avgYield;
        }
    }
}