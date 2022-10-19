using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVGathering.Calc.UI.ViewModels
{
    public interface IMainVm
    {
        int Attempts { get; set; }
        int AvailableGP { get; set; }
        int BaseYield { get; set; }
        double BoonChance { get; set; }
        int BountifulBonus { get; set; }
        ICommand CalculateCmd { get; }
        double Chance { get; set; }
        int CharacterLevel { get; set; }
        int EfficientGP { get; set; }
        ObservableCollection<ActionVm> EfficientRotation { get; }
        double EfficientYield { get; set; }
        double EfficientYieldPer100GP { get; set; }
        int HighestGP { get; set; }
        ObservableCollection<ActionVm> HighestRotation { get; }
        double HighestYield { get; set; }
        double HighestYieldPer100GP { get; set; }
        bool IsMiner { get; set; }
        double BaseTotalYield { get; set; }

        void LoadSettings(IMainSettings settings);
        void SaveSettings(IMainSettings settings);
    }
}