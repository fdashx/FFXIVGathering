using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Chance1 : BaseAction, IGatheringAction
    {
        public override string NameBotanist => "Field Mastery";
        public override string NameMiner => "Sharp Vision";
        public override bool IsRepeatable => false;

        public override int GP => 50;

        public override int Level => 4;

        public override void Execute(GatheringContext context)
        {
            context.Chance = Math.Min(context.Chance + 0.05, 1);

            base.Execute(context);
        }
    }
}