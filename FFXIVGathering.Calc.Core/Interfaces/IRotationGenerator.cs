using FFXIVGathering.Calc.Core.Data;

namespace FFXIVGathering.Calc.Core.Interfaces
{
    public interface IRotationGenerator
    {
        List<IRotation> GetRotations(GatheringContext initialContext);
    }
}