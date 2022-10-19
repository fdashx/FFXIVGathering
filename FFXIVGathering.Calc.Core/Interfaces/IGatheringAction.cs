using FFXIVGathering.Calc.Core.Data;

namespace FFXIVGathering.Calc.Core.Interfaces
{
    public interface IGatheringAction
    {
        int Level { get; }
        string NameBotanist { get; }
        string NameMiner { get; }
        bool IsRepeatable { get; }
        int GP { get; }
        bool CanExecute(GatheringContext context);
        void Execute(GatheringContext context);
    }
}