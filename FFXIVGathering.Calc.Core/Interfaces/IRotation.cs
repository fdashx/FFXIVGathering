using FFXIVGathering.Calc.Core.Data;

namespace FFXIVGathering.Calc.Core.Interfaces
{
    public interface IRotation
    {
        public IList<IGatheringAction> Actions { get; }
        public GatheringContext Context { get; }

        public void AddAction(IGatheringAction action);
    }
}
