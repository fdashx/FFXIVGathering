using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Chance3 : BaseAction, IGatheringAction
    {
        public override string NameBotanist => "Field Mastery III";

        public override string NameMiner => "Sharp Vision III";

        public override bool IsRepeatable => false;

        public override int GP => 250;

        public override int Level => 10;


        public override void Execute(GatheringContext context)
        {
            context.Chance = Math.Min(context.Chance + 0.5, 1);
            
            base.Execute(context);
        }
    }
}
