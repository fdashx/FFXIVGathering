using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Chance2 : BaseAction, IGatheringAction
    {
        public override string NameBotanist => "Field Mastery II";

        public override string NameMiner => "Sharp Vision II";

        public override bool IsRepeatable => false;

        public override int GP => 100;

        public override int Level => 5;

        public override int ExecutionOrder => 1;

        public override void Execute(GatheringContext context)
        {
            context.Chance = Math.Min(context.Chance + 0.15, 1);
            
            base.Execute(context);
        }
    }
}
