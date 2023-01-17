using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Bountiful2 : BaseAction, IGatheringAction
    {
        public override string NameBotanist => "Bountiful Harvest II";
        public override string NameMiner => "Bountiful Yield II";
        public override bool IsRepeatable => true;

        public override int GP => 100;

        public override int Level => 68;

        public override int ExecutionOrder => 3;

        public override void Execute(GatheringContext context)
        {
            context.BountifulAttempts++;

            base.Execute(context);
        }
    }
}
