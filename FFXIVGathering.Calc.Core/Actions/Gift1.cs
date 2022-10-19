using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Gift1 : BaseAction, IGatheringAction
    {
        public override int Level => 15;

        public override string NameBotanist => "Pioneer's Gift I";

        public override string NameMiner => "Mountaineer's Gift I";

        public override bool IsRepeatable => false;

        public override int GP => 50;

        public override void Execute(GatheringContext context)
        {
            context.Boon = Math.Min(context.Boon + 0.1, 1);

            base.Execute(context);
        }
    }
}
