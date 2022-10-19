using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Yield2 : BaseAction, IGatheringAction
    {
        public override int Level => 40;

        public override string NameBotanist => "Blessed Harvest II";

        public override string NameMiner => "King's Yield II";

        public override bool IsRepeatable => false;

        public override int GP => 500;

        public override void Execute(GatheringContext context)
        {
            context.BaseAmount += 2;

            base.Execute(context);
        }
    }
}
