using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class AttemptEnhanced : BaseAction, IGatheringAction
    {
        public override int Level => 90;

        public override string NameBotanist => "Ageless Words (Enhanced)";

        public override string NameMiner => "Solid Reason (Enhanced)";

        public override bool IsRepeatable => true;

        public override int GP => 300;

        public override void Execute(GatheringContext context)
        {
            context.Attempts++;
            context.WiseAttempts++;

            base.Execute(context);
        }
    }
}
