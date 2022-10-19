using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Data
{
    public class GatheringContext
    {
        public int AvailableGP { get; set; }
        public int BaseAmount { get; set; }
        public double Chance { get; set; }

        public int Attempts { get; set; }
        public double Boon { get; set; }
        public int BountifulBonus { get; set; }
        public int BoonBonus { get; set; }
        public int BountifulAttempts { get; set; }

        public int CharacterLevel { get; set; }

        public int WiseAttempts { get; set; }
    }
}