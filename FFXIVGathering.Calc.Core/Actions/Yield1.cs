using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Yield1 : BaseAction, IGatheringAction
    {
        public override int Level => 30;

        public override string NameBotanist => "Blessed Harvest";

        public override string NameMiner => "King's Yield";

        public override bool IsRepeatable => false;

        public override int GP => 400;

        public override int ExecutionOrder => 1;

        public override void Execute(GatheringContext context)
        {
            context.BaseAmount++;

            base.Execute(context);
        }
    }
}
