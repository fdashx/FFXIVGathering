using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Bountiful1 : BaseAction, IGatheringAction
    {
        public override string NameBotanist => "Bountiful Harvest";
        public override string NameMiner => "Bountiful Yield";
        public override bool IsRepeatable => true;

        public override int GP => 100;

        public override int Level => 24;

        public override bool CanExecute(GatheringContext context)
        {
            return base.CanExecute(context) && context.CharacterLevel < 68;
        }
        public override void Execute(GatheringContext context)
        {
            context.BountifulAttempts++;

            base.Execute(context);
        }
    }
}
