using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public class Attempt : BaseAction, IGatheringAction
    {
        public override int Level => 25;

        public override string NameBotanist => "Ageless Words";

        public override string NameMiner => "Solid Reason";

        public override bool IsRepeatable => true;

        public override int GP => 300;

        public override int ExecutionOrder => 2;

        public override bool CanExecute(GatheringContext context)
        {
            return base.CanExecute(context) && context.CharacterLevel < 90;
        }

        public override void Execute(GatheringContext context)
        {
            context.Attempts++;

            base.Execute(context);
        }
    }
}
