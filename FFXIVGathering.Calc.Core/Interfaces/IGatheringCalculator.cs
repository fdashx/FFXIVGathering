using FFXIVGathering.Calc.Core.Data;

namespace FFXIVGathering.Calc.Core.Interfaces
{
    public interface IGatheringCalculator
    {
        double CalculateYield(GatheringContext context);
        int CalculateGP(IEnumerable<IGatheringAction> actions);

        GatheringOutcome CalculateOutcome(IRotation rotation, GatheringOutcome? baseOutcome = null);
    }
}