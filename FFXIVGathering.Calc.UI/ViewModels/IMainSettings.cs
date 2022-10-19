using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.UI.ViewModels
{
    public interface IMainSettings
    {
        [DefaultValue(90)]
        int CharacterLevel { get; set; }
        [DefaultValue(900)]
        int AvailableGP { get; set; }
        [DefaultValue(4)]
        int GatheringAttempts { get; set; }
        [DefaultValue(1)]
        int BaseYieldPerAttempt { get; set; }
        [DefaultValue(1.0)]
        double GatheringChance { get; set; }
        [DefaultValue(0.6)]
        double BoonChance { get; set; }
        [DefaultValue(1)]
        int BoonBonusYield { get; set; }
        [DefaultValue(3)]
        int BountifulBonusYield { get; set; }
        [DefaultValue(true)]
        bool IsMiner { get; set; }
    }
}
