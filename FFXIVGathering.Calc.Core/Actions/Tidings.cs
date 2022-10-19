using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Tidings : BaseAction, IGatheringAction
    {
        public override int Level => 81;

        public override string NameBotanist => "Nophica's Tidings";

        public override string NameMiner => "Nald'thal's Tidings";

        public override bool IsRepeatable => false;

        public override int GP => 200;

        public override void Execute(GatheringContext context)
        {
            context.BoonBonus++;

            base.Execute(context);
        }
    }
}
