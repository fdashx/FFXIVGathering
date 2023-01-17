using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Gift2 : BaseAction, IGatheringAction
    {
        public override int Level => 50;

        public override string NameBotanist => "Pioneer's Gift II";

        public override string NameMiner => "Mountaineer's Gift II";

        public override bool IsRepeatable => false;

        public override int GP => 100;

        public override int ExecutionOrder => 1;

        public override void Execute(GatheringContext context)
        {
            context.Boon = Math.Min(context.Boon + 0.3, 1);

            base.Execute(context);
        }
    }
}
